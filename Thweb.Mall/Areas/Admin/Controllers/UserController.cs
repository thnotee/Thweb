using Microsoft.AspNetCore.Mvc;

namespace Thweb.Mall.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
