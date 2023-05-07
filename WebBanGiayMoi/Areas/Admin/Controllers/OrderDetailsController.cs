using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanGiayMoi.Models;

namespace WebBanGiayMoi.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/OrderDetails
        public ActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 50;
            var orderDetails = db.OrderDetails.Include(o => o.Giay).Include(o => o.Order).Include(o => o.Order.ApplicationUser);
            return View(orderDetails.ToList().OrderByDescending(id => id.Id).ToPagedList(page.Value, pageSize));
        }

        // GET: Admin/OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Include(o => o.Giay).Include(o => o.Order).Include(o => o.Order.ApplicationUser).ToList().FirstOrDefault(s => s.Id == id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Admin/OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Admin/OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
