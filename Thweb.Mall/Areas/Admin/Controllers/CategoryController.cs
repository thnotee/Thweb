using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;

namespace Thweb.Mall.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> CategoryList = await _unitOfWork.Category.GetAllAsync();
            return View(CategoryList);
        }

        public async Task<IActionResult> Upsert(int Id)
        {

            Category category = new Category();
            if (Id != 0) {
                category = await _unitOfWork.Category.GetAsync(x => x.Id == Id);
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                var data = await _unitOfWork.Category.GetAsync(x => x.Id == category.Id);
                if (data == null)
                {
                    await _unitOfWork.Category.AddAsync(category);
                    TempData["successMsg"] = "카테고리 추가 성공!!";
                }
                else 
                {
                    
                    if (data != null)
                    {
                        _unitOfWork.Category.Update(category);
                        TempData["successMsg"] = "카테고리 수정 성공!!";
                    }
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            TempData["errorMsg"] = "카테고리 추가 or 수정 실패!!";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int categoryId)
        {

            var data = await _unitOfWork.Category.GetAsync(x => x.Id == categoryId);
            if (data != null)
            {
                _unitOfWork.Category.Remove(data);
                _unitOfWork.Save();
                TempData["successMsg"] = "카테고리 삭제 성공!!.";
            }
            else 
            {
                TempData["errorMsg"] = "카테고리 삭제 실패!!";
            }
         
            return RedirectToAction("Index");
            
        }

    }
}
