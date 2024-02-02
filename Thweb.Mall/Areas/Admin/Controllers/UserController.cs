using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thweb.Data.Repository;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;

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

        public async Task<IActionResult> Index()
        {

            var userList = await _unitOfWork.ThwebUser.GetPagedListAsync(1,10);
            //List<ThwebUser> userList = _unitOfWork.ThwebUser.GetAll(includeProperties: "Company").ToList();
            return View(userList);
        }

    }
}
