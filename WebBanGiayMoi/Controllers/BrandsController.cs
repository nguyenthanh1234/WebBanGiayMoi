using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanGiayMoi.Models;

namespace WebBanGiayMoi.Controllers
{
    public class BrandsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Brands
        public ActionResult Detail(int id)
        {

            Giay vitri = db.Giays.ToList().Find(x => x.Id == id);
            return View(vitri);
        }
        public ActionResult Adidas(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 4;
            return View(db.Giays.ToList().Where(m => m.BrandId == 1).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }
        public ActionResult Nike(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 4;
            return View(db.Giays.ToList().Where(m => m.BrandId == 2).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }
        public ActionResult Mlb(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 4;
            return View(db.Giays.ToList().Where(m => m.BrandId == 3).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }
        public ActionResult Converse(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 4;
            return View(db.Giays.ToList().Where(m => m.BrandId == 4).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }
        public ActionResult Vans(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 4;
            return View(db.Giays.ToList().Where(m => m.BrandId == 5).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }
        public ActionResult Valentino(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 4;
            return View(db.Giays.ToList().Where(m => m.BrandId == 6).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }
    }
}