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
            Expression<Func<ThwebUser, DateTime>> orderByEx = x => x.RegDate;
            PagedList<ThwebUser> userList = await _unitOfWork.ThwebUser.GetPagedListAsync<DateTime>(page, 10,null,orderBy: orderByEx);
            userList.pagerOptions.Path = "/Admin/User/Index";
            userList.pagerOptions.AddQueryString = $"searchString={searchString}";
            return View(userList);
        }

    }
}
