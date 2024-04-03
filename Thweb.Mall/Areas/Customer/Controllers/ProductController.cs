using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
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


        public async Task<IActionResult> Index(int categoryId = 1, int page = 1)
        {
            Expression<Func<Product, bool>>? filter = u=>u.CategoryId == categoryId;

            Expression<Func<Product, DateTime>> orderByEx = x => x.RegDate;
            var productList = await _unitOfWork.Product.GetPagedListAsync<DateTime>(
                page: page
                , pageSize: 100
                , filter: filter
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

        public async Task<IActionResult> Detail(int id)
        {
            var product = new Product();
            if (id != 0) 
            {
                product = await _unitOfWork.Product.GetAsync(x => x.Id == id);
                Expression<Func<Image, bool>> tableName = x => (x.TableName == "Product" && x.TableId == id);
                product.Images = await _unitOfWork.Image.GetAllAsync(tableName);

            }

            IEnumerable<Product> randomRelatedProducts = new List<Product>();
            //관련상품 가져오기
            if (product.CategoryId != 0)
            {
                Expression<Func<Product, bool>>? filter = u => u.CategoryId == product.CategoryId;
                IEnumerable<Product> relatedProducts = await _unitOfWork.Product.GetAllAsync(filter);

                if (relatedProducts.Any())
                {
                    if (relatedProducts.Count() > 3)
                    {
                        randomRelatedProducts = relatedProducts
                        .OrderBy(p => Guid.NewGuid())
                        .Take(3)
                        .ToList();
                    }
                    else 
                    {
                        randomRelatedProducts = relatedProducts;
                    }
                   
                    //이미지 매핑
                    foreach (var item in randomRelatedProducts)
                    {
                        Expression<Func<Image, bool>> tableName = x => (x.TableName == "Product" && x.TableId == item.Id);
                        item.Images = await _unitOfWork.Image.GetAllAsync(tableName);
                    }
                }
      
            }
            ViewData["relatedProducts"] = randomRelatedProducts;

            return View(product);
        }

    
    }
}
