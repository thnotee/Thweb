using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;

namespace Thweb.Mall.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page =1)
        {
            Expression<Func<Product, DateTime>> orderByEx = x => x.RegDate;
            IEnumerable<Product> ProductList = await _unitOfWork.Product.GetPagedListAsync<DateTime>(page, 10, null, orderByEx);
            return View(ProductList);
        }

        public async Task<IActionResult> Upsert(int Id)
        {
            Product product = new Product();
            if (Id != 0)
            {
                product = await _unitOfWork.Product.GetAsync(x => x.Id == Id);
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                var data = await _unitOfWork.Product.GetAsync(x => x.Id == product.Id);
                if (data == null)
                {
                    await _unitOfWork.Product.AddAsync(product);
                    TempData["successMsg"] = "상품 추가 성공!!";
                }
                else
                {

                    if (data != null)
                    {
                        _unitOfWork.Product.Update(product);
                        TempData["successMsg"] = "상품 수정 성공!!";
                    }
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            TempData["errorMsg"] = "상품 추가 or 수정 실패!!";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {

            var data = await _unitOfWork.Category.GetAsync(x => x.Id == productId);
            if (data != null)
            {
                _unitOfWork.Category.Remove(data);
                _unitOfWork.Save();
                TempData["successMsg"] = "상품 삭제 성공!!.";
            }
            else
            {
                TempData["errorMsg"] = "상품 삭제 실패!!";
            }

            return RedirectToAction("Index");

        }
    }
}
