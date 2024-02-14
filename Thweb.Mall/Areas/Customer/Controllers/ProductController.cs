using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;

namespace Thweb.Mall.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            Expression<Func<Product, DateTime>> orderByEx = x => x.RegDate;
            var productList = await _unitOfWork.Product.GetPagedListAsync<DateTime>(
                page: page
                , pageSize: 10
                , filter: null
                , orderBy: orderByEx
                , descending: false
            , includeProperties: "Category");


            foreach (var item in productList)
            {
                Expression<Func<Image, bool>> tableName = x => (x.TableName == "Product" && x.TableId == item.Id);
                item.Images = await _unitOfWork.Image.GetAllAsync(tableName);
            }

            return View(productList);
        }

        public IActionResult Detail()
        {

            return View();
        }
    }
}
