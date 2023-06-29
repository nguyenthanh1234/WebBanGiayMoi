using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebBanGiayMoi.Models;

namespace WebBanGiayMoi.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Home
        public ActionResult Index(int? page, string searchString)
        {
            ViewBag.Keyword = searchString;
            if (page == null)
                page = 1;
            int pageSize = 50;
            return View(Giay.GetAll(searchString).OrderByDescending(s => s.Id).ToPagedList(page.Value, pageSize));
        }

        // GET: Giays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giay giay = db.Giays.Include(o => o.Category).Include(o => o.Brand).ToList().Find(x => x.Id == id);
            if (giay == null)
            {
                return HttpNotFound();
            }
            return View(giay);
        }

        // GET: Admin/Giays/Create
        public ActionResult Create()
        {

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Giays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Gia,SoLuong,Hinh,Content,CategoryId,BrandId")] Giay giay)
        {
            if (ModelState.IsValid)
            {
                db.Giays.Add(giay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", giay.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", giay.CategoryId);
            return View(giay);
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Giay giay = db.Giays.Find(id);

            if (giay == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", giay.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", giay.CategoryId);
            return View(giay);
        }

        // POST: Admin/Giays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Gia,SoLuong,Hinh,Content,CategoryId,BrandId")] Giay giay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", giay.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", giay.CategoryId);
            return View(giay);
        }

        // GET: Giays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giay giay = db.Giays.Find(id);
            if (giay == null)
            {
                return HttpNotFound();
            }
            return View(giay);
        }

        // POST: Giays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Giay giay = db.Giays.Find(id);

            db.Giays.Remove(giay);
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
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}

