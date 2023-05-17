using Microsoft.AspNet.Identity;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebBanGiayMoi.Helper;
using WebBanGiayMoi.Models;
using WebBanGiayMoi.Models.VNPAY;

namespace WebBanGiayMoi.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public double tyGia = 23500;

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

        public ActionResult AddtoCart(FormCollection form)
        {
            int id = int.Parse(form["idpro"]);
            var pro = _db.Giays.SingleOrDefault(x => x.Id == id);
            if (pro != null)
            {
                GetCart().Add(pro, int.Parse(form["soLuong"]), int.Parse(form["size"]));
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        //Trang giỏ hàng
        public ActionResult ShowToCart()
        {
            if(Session["Profile"] == null)
            {
                return RedirectToAction("login", "Account");
            }

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
                cart.Update_Quantity_Shopping(id_pro, quantity, int.Parse(form["Size"]));
                return RedirectToAction("ShowToCart", "ShoppingCart");
            }
            return RedirectToAction("ShowToCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(int.Parse(form["Id_Product"]), int.Parse(form["Size"]));
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
        public ActionResult Shopping_Failure()
        {
            return View();
        }

        //đặt hàng
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                if(cart.Items.Count() == 0)
                {
                    return RedirectToAction("index", "home");
                }
                Models.Order _order = new Models.Order();
                _order.OrderDate = DateTime.Now;
                if (Session["Profile"] == null)
                {
                    return RedirectToAction("login", "Account");
                }
                var userCurrent = Session["Profile"] as ApplicationUser;
                _order.ApplicationUserId = userCurrent.Id;
                _order.Descriptions = userCurrent.Address;
                _order.PhoneKH = userCurrent.Phone;
                _order.PhuongThucTT = "COD";
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
                    _order_Detail.Size = item._shopping_size;
                    _db.OrderDetails.Add(_order_Detail);
                }
                _db.SaveChanges();
                var gioHangCuaToi = Session["Cart"] as Cart;
                SendMail.SendEmail(/*form["Email"]*/ userCurrent.Email, "Cảm ơn bạn đã mua hàng tại website giày pro",
               "Tổng số tiền là: " + gioHangCuaToi.Total_Money() + " - Chúc bạn mua sắm vui vẻ!!!", "");
                cart.ClearCart();

                return RedirectToAction("Shopping_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Đã xảy ra lỗi, mời bạn kiểm tra lại");
            }
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/shoppingcart/paymentwithpaypal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Shopping_Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Shopping_Failure");
            }
            #region
            try
            {
                Cart cart = Session["Cart"] as Cart;
               
                Models.Order _order = new Models.Order();
                _order.OrderDate = DateTime.Now;
                if (Session["Profile"] == null)
                {
                    return RedirectToAction("login", "Account");
                }
                var userCurrent = Session["Profile"] as ApplicationUser;
                _order.ApplicationUserId = userCurrent.Id;
                _order.Descriptions = userCurrent.Address;
                _order.PhoneKH = userCurrent.Phone;
                _order.PhuongThucTT = "PayPal";
                _db.Orders.Add(_order);
                foreach (var item in cart.Items)
                {
                    OrderDetail _order_Detail = new OrderDetail();
                    _order_Detail.OrderId = _order.Id;
                    _order_Detail.GiayId = item._shopping_product.Id;
                    _order_Detail.UnitPriceSale = item._shopping_product.Gia;
                    _order_Detail.QuantitySale = item._shopping_quantity;
                    _order_Detail.Size = item._shopping_size;
                    _db.OrderDetails.Add(_order_Detail);
                }
                _db.SaveChanges();
                var gioHangCuaToi = Session["Cart"] as Cart;
                SendMail.SendEmail(/*form["Email"]*/ userCurrent.Email, "Cảm ơn bạn đã mua hàng tại website giày pro",
               "Tổng số tiền là: " + gioHangCuaToi.Total_Money() + " - Chúc bạn mua sắm vui vẻ!!!", "");
                cart.ClearCart();
                return RedirectToAction("Shopping_Success", "ShoppingCart");
            }
            catch
            {
                return Content("Đã xảy ra lỗi, mời bạn kiểm tra lại");
            }
            #endregion
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var gioHangCuaToi = Session["Cart"] as Cart;
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  

           var tongGia = gioHangCuaToi.Total_MoneyUSD();
            foreach (var item in gioHangCuaToi.Items)
            {
                itemList.items.Add(new Item()
                {
                    name = item._shopping_product.Name,
                    currency = "USD",                 
                    price = Math.Round(item._shopping_product.Gia / tyGia,2).ToString(),
                    quantity = item._shopping_quantity.ToString(),
                    sku = "sku"
                });

            }
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  


            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = tongGia.ToString()
            };
            //Final amount with details  

            var amount = new Amount()
            {
                currency = "USD",
                total = tongGia.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }


        //VNPAY
        public ActionResult Payment()
        {
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();
            Cart cart = Session["Cart"] as Cart;


            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", (cart.Total_Money() * 100).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }


        public ActionResult PaymentConfirm(FormCollection form)
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                        try
                        {
                            Cart cart = Session["Cart"] as Cart;
                            if (cart.Items.Count() == 0)
                            {
                                return RedirectToAction("index", "home");
                            }
                            Models.Order _order = new Models.Order();
                            _order.OrderDate = DateTime.Now;
                            if (Session["Profile"] == null)
                            {
                                return RedirectToAction("login", "Account");
                            }
                            var userCurrent = Session["Profile"] as ApplicationUser;
                            _order.ApplicationUserId = userCurrent.Id;
                            _order.Descriptions = userCurrent.Address;
                            _order.PhoneKH = userCurrent.Phone;
                            _order.PhuongThucTT = "VNPAY";

                            _db.Orders.Add(_order);
                            foreach (var item in cart.Items)
                            {
                                OrderDetail _order_Detail = new OrderDetail();
                                _order_Detail.OrderId = _order.Id;
                                _order_Detail.GiayId = item._shopping_product.Id;
                                _order_Detail.UnitPriceSale = item._shopping_product.Gia;
                                _order_Detail.QuantitySale = item._shopping_quantity;
                                _order_Detail.Size = item._shopping_size;
                                _db.OrderDetails.Add(_order_Detail);
                            }
                            _db.SaveChanges();
                            var gioHangCuaToi = Session["Cart"] as Cart;
                            SendMail.SendEmail(/*form["Email"]*/ userCurrent.Email, "Cảm ơn bạn đã mua hàng tại website giày pro",
                           "Tổng số tiền là: " + gioHangCuaToi.Total_Money() + " - Chúc bạn mua sắm vui vẻ!!!", "");
                            cart.ClearCart();

                            return RedirectToAction("Shopping_Success", "ShoppingCart");
                        }
                        catch
                        {
                            return Content("Đã xảy ra lỗi, mời bạn kiểm tra lại");
                        }
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View("Shopping_Failure");
        }
    }
}