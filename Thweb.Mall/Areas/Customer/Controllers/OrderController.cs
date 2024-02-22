using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;

namespace Thweb.Mall.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<Cart> cartList = await _unitOfWork.Cart.GetAllAsync(u => u.UserId == userId, includeProperties: "Product");

            if (cartList != null)
            {
                foreach (var item in cartList)
                {
                    Expression<Func<Image, bool>> tableName = x => (x.TableName == "Product" && x.TableId == item.Product.Id);
                    item.Product.Images = await _unitOfWork.Image.GetAllAsync(tableName);
                }
            }
            return View(cartList);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] KaKaoPay kaKaopay) {
            //OrderHeadr에 넣기
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<Cart> cartList = await _unitOfWork.Cart.GetAllAsync(u => u.UserId == userId, includeProperties: "Product");
            //UserId를 가져와서 장바구니 넣기
            return Json(new { seccess = true, message = "주문 추가 성공" });
        }
    }
}
