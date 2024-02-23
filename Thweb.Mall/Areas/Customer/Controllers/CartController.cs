using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;
using Thweb.Data.Repository;
using Thweb.Data.Repository.IRepository;
using Thweb.Model.Model;

namespace Thweb.Mall.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<Cart> cartList = await _unitOfWork.Cart.GetAllAsync(u => u.UserId == userId, includeProperties: "Product");

            if (cartList != null) {

                foreach (var item in cartList)
                {
                    Expression<Func<Image, bool>> tableName = x => (x.TableName == "Product" && x.TableId == item.Product.Id);
                    item.Product.Images = await _unitOfWork.Image.GetAllAsync(tableName);
                }
            }
            return View(cartList);
        }


        /// <summary>
        /// API CALL
        /// </summary>

        public async Task<IActionResult> AddToCart(int productId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = await _unitOfWork.Cart.GetAsync(u => u.UserId == userId && u.ProductId == productId);

            if (cart != null)
            {
                cart.Count += cart.Count;
                _unitOfWork.Cart.Update(cart);
                _unitOfWork.Save();
            }
            else
            {
                cart = new Cart();
                cart.UserId = userId;
                cart.ProductId = productId;
                cart.Count = 1;
                await _unitOfWork.Cart.AddAsync(cart);
                _unitOfWork.Save();
            }
            return Json(new { seccess = true, message = "장바구니 추가 성공" });
        }



        public async Task<IActionResult> Plus(int cartId)
        {
            
            var cart = await _unitOfWork.Cart.GetAsync(u => u.Id == cartId);
             cart.Count += 1;
            _unitOfWork.Cart.Update(cart);
            _unitOfWork.Save();

            var orderTotal = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<Cart> cartList = await _unitOfWork.Cart.GetAllAsync(u => u.UserId == userId, includeProperties: "Product");
            if (cartList != null)
            {
                orderTotal = cartList.Sum(item => item.Count * item.Product.Price);
            }

            var data = new { orderTotal = orderTotal, cartCount = cart.Count };
            /*HttpContext.Session.SetInt32(SD.SessionCart,
                   _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() + 1);*/
            return Json(new { seccess = true, data });

        }

        public async Task<IActionResult> Minus(int cartId)
        {

            var cart = await _unitOfWork.Cart.GetAsync(u => u.Id == cartId);
            cart.Count -= 1;
            _unitOfWork.Cart.Update(cart);
            _unitOfWork.Save();

            var orderTotal = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<Cart> cartList = await _unitOfWork.Cart.GetAllAsync(u => u.UserId == userId, includeProperties: "Product");
            if (cartList != null) {
                orderTotal = cartList.Sum(item => item.Count * item.Product.Price);
            }

            var data = new { orderTotal = orderTotal, cartCount = cart.Count };
            /*HttpContext.Session.SetInt32(SD.SessionCart,
                   _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() + 1);*/
            return Json(new { seccess = true, data });

        }

  
        public async Task<IActionResult> Remove(int cartId)
        {
            var cart = await _unitOfWork.Cart.GetAsync(u => u.Id == cartId);
            _unitOfWork.Cart.Remove(cart);
            _unitOfWork.Save();

            var orderTotal = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<Cart> cartList = await _unitOfWork.Cart.GetAllAsync(u => u.UserId == userId, includeProperties: "Product");
            if (cartList != null)
            {
                orderTotal = cartList.Sum(item => item.Count * item.Product.Price);
            }

            /*
            HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);
            
            */
            return Json(new { seccess = true, data = cartId });
        }
        
    }
}
