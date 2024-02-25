using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using Thweb.Data.Repository;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;
using Thweb.Model.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Thweb.Mall.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string searchString ="", int page = 1)
        {
            Expression<Func<Product, bool>>? filter = null;
            if (!string.IsNullOrEmpty(searchString))
            {
                filter = u => (u.Title.Contains(searchString));
            }
            

            Expression<Func<Product, DateTime>> orderByEx = x => x.RegDate;
            var productList = await _unitOfWork.Product.GetPagedListAsync<DateTime>(
                page: page
                , pageSize: 10
                , filter: filter
                , orderBy: orderByEx
                , descending: false
            , includeProperties: "Category");

            foreach (var item in productList)
            {
                Expression<Func<Image, bool>> tableName = x => (x.TableName == "Product" && x.TableId == item.Id);
                item.Images = await _unitOfWork.Image.GetAllAsync(tableName);
            }

            productList.pagerOptions.Path = "/Admin/Product/Index";
            productList.pagerOptions.AddQueryString = $"searchString={searchString}";
            return View(productList);
        }

        public async Task<IActionResult> Upsert(int id)
        {
            ProductVm productVm = new ProductVm
            {
                CategoryList = (await _unitOfWork.Category.GetAllAsync()).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };

            if (id != 0)
            {
                productVm.Product = await _unitOfWork.Product.GetAsync(x => x.Id == id);
                Expression<Func<Image, bool>> tableName = x => (x.TableName == "Product" && x.TableId == id);
                productVm.Product.Images = await _unitOfWork.Image.GetAllAsync(tableName);
            }

            return View(productVm);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Product product, List<IFormFile>? files)
        {
            product.RegDate = DateTime.Now;
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


                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (files != null)
                {
                    foreach (IFormFile file in files)
                    {
                        string originFileName = Path.GetFileName(file.FileName);
                        string savefileName = Guid.NewGuid().ToString() + originFileName; //중복 회피를 위해
                        string imgPath = @"images\product-" + product.Id;
                        string finalPath = Path.Combine(wwwRootPath, imgPath);
                        if (!Directory.Exists(finalPath)) { Directory.CreateDirectory(finalPath); } //폴더생성
                        using (var fileStream = new FileStream(Path.Combine(finalPath, savefileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        Image image = new Image();
                        image.DirectoryPath = @"\" + imgPath + @"\";
                        image.OriginFileName = originFileName;
                        image.SaveFileName = savefileName;
                        image.TableName = "Product";
                        image.TableId = product.Id; // SQL SERVER ID값 먼저 가져오는방법
                        await _unitOfWork.Image.AddAsync(image);
                    }
                    _unitOfWork.Save();

                }
                return RedirectToAction("Index");
            }

            TempData["errorMsg"] = "상품 추가 or 수정 실패!!";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int productId)
        {

            var data = await _unitOfWork.Product.GetAsync(x => x.Id == productId);
            if (data != null)
            {
                Expression<Func<Image, bool>> tableName = x => (x.TableName == "Product" && x.TableId == data.Id);
                var ImageList = await _unitOfWork.Image.GetAllAsync(tableName);
                _unitOfWork.Image.RemoveRange(ImageList);

                if (ImageList != null)
                {
                    foreach (var itme in ImageList)
                    {
                        var directoryPath = itme.DirectoryPath.TrimStart('\\').TrimEnd('\\'); //앞 뒤 역슬래쉬 제거
                        string finalPath = Path.Combine(_webHostEnvironment.WebRootPath, directoryPath);
                        if (Directory.Exists(finalPath))
                        {
                            string[] filePaths = Directory.GetFiles(finalPath);
                            foreach (string filePath in filePaths)
                            {
                                System.IO.File.Delete(filePath);
                            }
                            Directory.Delete(finalPath);
                        }
                    }

                }

                _unitOfWork.Product.Remove(data);
                _unitOfWork.Save();
                TempData["successMsg"] = "상품 삭제 성공!!.";
            }
            else
            {
                TempData["errorMsg"] = "데이터가 존재하지 않습니다.!!";
            }

            return RedirectToAction("Index");

        }


        [HttpPost]
        public IActionResult ImageUpload(IFormFile? file)
        {
            
            var imagePath = "";
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                
                    string originFileName = Path.GetFileName(file.FileName);
                    string savefileName = Guid.NewGuid().ToString() + originFileName; //중복 회피를 위해
                    string imgPath = @"images\uploadImg-" + DateTime.Now.ToString("yyyyMM");
                    string finalPath = Path.Combine(wwwRootPath, imgPath);
                    if (!Directory.Exists(finalPath)) { Directory.CreateDirectory(finalPath); } //폴더생성
                    using (var fileStream = new FileStream(Path.Combine(finalPath, savefileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    imagePath = @"\" + imgPath + @"\"+ savefileName;
                
            }
            return Json(new { location = imagePath });

         }

    }
}

