using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebBanGiayMoi.Helper;
using WebBanGiayMoi.Models;

namespace WebBanGiayMoi.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        // GET: ShoppingCart

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ActionResult AddtoCart(int id)
        {
            var pro = _db.Giays.SingleOrDefault(x => x.Id == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        //Trang giỏ hàng
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("ShowToCart", "ShoppingCart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }

        //Cap nhap so luong san pham
        public ActionResult Update_Quantity_Cart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["Id_Product"]);
            if (form["Quantity"] != "")
            {
                int quantity = int.Parse(form["Quantity"]);
                cart.Update_Quantity_Shopping(id_pro, quantity);
                return RedirectToAction("ShowToCart", "ShoppingCart");
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }

        /*Hiển thị tren icon giỏ hàng*/
        public PartialViewResult BagCart()
        {
            int _t_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                _t_item = cart.Total_Quantity();
            }
            ViewBag.infoCart = _t_item;
            return PartialView("BagCart");
        }
        //Đặt hàng
        public ActionResult Shopping_Success()
        {
            return View();
        }

        //đặt hàng
        public ActionResult CheckOut(FormCollection form)
        {
            IEnumerable<ApplicationUser> user = _db.Users.ToList();
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();
                _order.OrderDate = DateTime.Now;
                if (Session["Profile"] == null)
                {
                    return RedirectToAction("login", "Account");
                }
                var userCurrent = Session["Profile"] as ApplicationUser;
                _order.ApplicationUserId = userCurrent.Id;
                _order.Descriptions = userCurrent.Address;

                //foreach (var item in user)
                //{
                //    if (item.Email == form["Email"])
                //    {
                //        _order.ApplicationUserId = item.Id;
                //        _order.Descriptions = item.Address;
                //    }
                //}
                _db.Orders.Add(_order);
                foreach (var item in cart.Items)
                {
                    OrderDetail _order_Detail = new OrderDetail();
                    _order_Detail.OrderId = _order.Id;
                    _order_Detail.GiayId = item._shopping_product.Id;
                    _order_Detail.UnitPriceSale = item._shopping_product.Gia;
                    _order_Detail.QuantitySale = item._shopping_quantity;
                    _db.OrderDetails.Add(_order_Detail);
                }
                _db.SaveChanges();
                var gioHangCuaToi = Session["Cart"] as Cart;
                SendMail.SendEmail(/*form["Email"]*/ userCurrent.Email, "Đơn hàng số: " + _order.Id + " tại website giàypro",
               "Tổng số tiền là: " + gioHangCuaToi.Total_Money() + " - Cảm ơn bạn đã mua hàng!!!", "");
                cart.ClearCart();

                return RedirectToAction("Shopping_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Lỗi rồi bạn ơi, Mời bạn kiểm tra lại thông tin đã điền");
            }
        }

    }
}