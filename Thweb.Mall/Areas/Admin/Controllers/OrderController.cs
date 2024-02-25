using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using System.Security.Claims;
using Thweb.Data.Repository;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;
using Thweb.Model.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Thweb.Mall.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OrderController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int page = 1,string searchString ="")
        {
            Expression<Func<OrderHeader, bool>>? filter = null;
            if (!string.IsNullOrEmpty(searchString))
            {
                filter = u => (u.OrderNo.Contains(searchString));
            }

           var orderList = await _unitOfWork.OrderHeader
                                .GetPagedListAsync<DateTime>(1, 100, filter, x => x.OrderDate, false, includeProperties: "OrderDetails,OrderDetails.Product");


            orderList.pagerOptions.Path = "/Admin/Order/Index";
            orderList.pagerOptions.AddQueryString = $"searchString={searchString}";
            return View(orderList);
        }

        public async Task<IActionResult> Detail(string key)
        {
            OrderHeader orderHeader = await _unitOfWork.OrderHeader
                                .GetAsync(u => u.OrderNo == key, includeProperties: "OrderDetails,OrderDetails.Product");

            return View(orderHeader);
        }

        [HttpPost]
        public async Task<IActionResult> Detail(OrderHeader editOrderHeader)
        {
            OrderHeader orderHeader = await _unitOfWork.OrderHeader
                              .GetAsync(u => u.OrderNo == editOrderHeader.OrderNo, includeProperties: "OrderDetails,OrderDetails.Product");

            orderHeader.Carrier = editOrderHeader.Carrier;
            orderHeader.TrackingNumber = editOrderHeader?.TrackingNumber;
            orderHeader.BuyerName = editOrderHeader.BuyerName;
            orderHeader.BuyerPhoneNumber = editOrderHeader.BuyerPhoneNumber;
            orderHeader.BuyerPostCode = editOrderHeader.BuyerPostCode;
            orderHeader.BuyerPostAddr = editOrderHeader.BuyerPostAddr;
            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.Save();
            TempData["successMsg"] = "주문수정 성공!!";
            return View(orderHeader);
        }


    }
}

