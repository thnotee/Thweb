using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Linq.Expressions;
using System.Xml;
using Thweb.Data.Repository;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;
using Thweb.Model.Model.Pager;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Thweb.Mall.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {

        private readonly UserManager<ThwebUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(UserManager<ThwebUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(int page = 1 , string searchString = "")
        {
            Expression<Func<ThwebUser, bool>>? filter = null;
            
            if (!string.IsNullOrEmpty(searchString)) 
            {
                DateTime searchDate;
                bool isDate = DateTime.TryParse(searchString, out searchDate);
                if (isDate) //날짜검색 시
                {
                    filter = u => (u.RegDate.Date == searchDate.Date);
                }
                else 
                {
                    filter = u => (u.Email.Contains(searchString) || u.Nickname.Contains(searchString));
                }
                
            }
            Expression<Func<ThwebUser, DateTime>> orderByEx = x => x.RegDate;
            PagedList<ThwebUser> userList = await _unitOfWork.ThwebUser.GetPagedListAsync<DateTime>(page, 10, filter, orderBy: orderByEx,true);
            userList.pagerOptions.Path = "/Admin/User/Index";
            userList.pagerOptions.AddQueryString = $"searchString={searchString}";
            return View(userList);
        }


        ////////////////////
        /// API Call
        ///////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LockUnlock([FromBody] string id)
        {
            var objFromDb = await _unitOfWork.ThwebUser.GetAsync(u => u.Id == id);
            var lockTf = false;
            
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1);
                lockTf = true;
            }
            _unitOfWork.ThwebUser.Update(objFromDb);

            string lockoutEndString = "";
            if (objFromDb.LockoutEnd != null)
            {
                lockoutEndString =  objFromDb.LockoutEnd.Value.UtcDateTime.ToString("yyyy-MM-dd");
            }
            
            _unitOfWork.Save();
            var data = new {
                id = objFromDb.Id,
                lockoutEnd = lockoutEndString,
                lockTf = lockTf
            };
            return Json(new { success = true, data= data });
        }

    }
}
