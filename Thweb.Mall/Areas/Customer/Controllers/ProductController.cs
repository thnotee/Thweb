using Microsoft.AspNetCore.Mvc;

namespace Thweb.Mall.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {

            return View();
        }
    }
}
