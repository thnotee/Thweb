using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using System.Linq.Expressions;
using System.Security.Claims;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;
using Thweb.Model.ViewModel;

namespace Thweb.Mall.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public ShoppingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<OrderHeader> orderList = await _unitOfWork.OrderHeader
                                .GetPagedListAsync<DateTime>(1, 100, u => u.ThwebUserId == userId, x => x.OrderDate, false, includeProperties: "OrderDetails,OrderDetails.Product");

            return View(orderList);
        }

        
        public async Task<IActionResult> AddOrder(KaKaoPay kaKaopay) {
            //OrderHeadr에 넣기
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            OrderHeader orderHeader = new OrderHeader();
            orderHeader.OrderNo = kaKaopay.imp_uid;
            orderHeader.ThwebUserId = userId;
            orderHeader.OrderDate = DateTime.Now;
            orderHeader.State = kaKaopay.status;
            orderHeader.BuyerPhoneNumber = kaKaopay.buyer_tel;
            orderHeader.BuyerPostCode = kaKaopay.buyer_postcode;
            orderHeader.BuyerPostAddr = kaKaopay.buyer_addr;
            orderHeader.BuyerName = kaKaopay.buyer_name;
            orderHeader.PaidAmount = kaKaopay.paid_amount;
            orderHeader.PaidAt = kaKaopay.paid_at.ToString();
            orderHeader.Name = kaKaopay.name;
            
            IEnumerable<Cart> cartList = await _unitOfWork.Cart.GetAllAsync(u => u.UserId == userId, includeProperties: "Product");
            //UserId를 가져와서 장바구니 넣기

            await _unitOfWork.OrderHeader.AddAsync(orderHeader);
            foreach (var cart in cartList)
            {
                
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = kaKaopay.imp_uid,
                    Price = cart.Product.Price,
                    Count = cart.Count
                };

               await _unitOfWork.OrderDetail.AddAsync(orderDetail);
            }
            _unitOfWork.Save();
            return Json(new { seccess = true, message = "주문 추가 성공" });
        }
    }
}
