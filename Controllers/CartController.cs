using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLCoffee.Models;
using QLCoffee.Models.ViewModel;

namespace QLCoffee.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();


        //Hàm lấy dịch vụ giỏ hàng
        private CartService GetCartService()
        {
            return new CartService(Session);

        }


        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }

        //Thêm sản phẩm vào giỏ hàng       
        public ActionResult AddToCart(string id, int quantity = 1)
        {
            //try
            //{
            //    if(quantity <= 0)
            //    {

            //    }
            //}
            var product = db.SANPHAMs.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            if (quantity <= 0)
            {
                quantity = 1;
            }

            var cartService = GetCartService();
            cartService.GetCart().AddItem(product.MaSP, product.PRODUCT.Img1, product.TenSP, product.GiaSP, quantity);
            ///                                     
            return RedirectToAction("Index");
        }
        //Xóa sản phẩm khỏi giỏ hàng
        public ActionResult RemoveFromCart(string id)
        {
            var cartService = GetCartService();
            cartService.GetCart().RemoveItem(id);
            return RedirectToAction("Index");

        }
        //Làm trống giỏ hàng
        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult UpdateQuantity(string id, int quantity)
        {
            var cartService = GetCartService();
            cartService.GetCart().UpdateQuantity(id, quantity);
            return RedirectToAction("Index");

        }


        //[HttpPost] 
        //public ActionResult AddToCart(string productId, int quantity)
        //{
        //    var product = db.SANPHAMs.FirstOrDefault(p => p.MaSP == productId);

        //    var cartItem = new CartItem
        //    {
        //        MaSP = product.MaSP,
        //        TenSP = product.TenSP,
        //        UnitPrice = product.GiaSP,
        //        Quantity = quantity
        //    };

        //    var cart = GetCart();
        //    cart.AddItem(cartItem);

        //    return View("Index");
        //}




        //public ActionResult RemoveFromCart(string productId)
        //{
        //    var cart = GetCart();
        //    cart.RemoveItem(productId);
        //    return RedirectToAction("Index");
        //}

        //public Cart GetCart()
        //{
        //    var cart = Session["Cart"] as Cart;
        //    if(cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;

        //    }
        //    return cart;
        //}

    }
}
