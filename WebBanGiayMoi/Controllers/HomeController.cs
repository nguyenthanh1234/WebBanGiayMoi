﻿using PagedList;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebBanGiayMoi.Models;

namespace WebBanGiayMoi.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 8;
            ViewBag.HienThiBaiViet = db.blog.ToList().OrderByDescending(m => m.ID).ToPagedList(1, 4);
            return View(db.Giays.ToList().OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
            
        }
        public ActionResult SanPham(int? page, string searchString)
        {
            ViewBag.Keyword = searchString;
            if (page == null)
                page = 1;
            int pageSize = 8;
            return View(Giay.GetAll(searchString).ToPagedList(page.Value, pageSize));
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Detail(int id)
        {

            Giay vitri = db.Giays.Include(o => o.Category).Include(o => o.Brand).ToList().Find(x => x.Id == id);

            return View(vitri);
        }
        public ActionResult HoaDon()
        {
            if (Session["Profile"] == null)
            {
                return RedirectToAction("login", "Account");
            }
            var orderDetails = db.OrderDetails.Include(o => o.Giay).Include(o => o.Order);
            return View(orderDetails.OrderByDescending(s => s.Id).ToList());
        }
        public ActionResult News(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 8;
            return View(db.blog.ToList().OrderByDescending(x => x.ID).ToPagedList(page.Value, pageSize));
        }
        public ActionResult DetailNews(int id)
        {
            News baiviettaivitri = db.blog.ToList().Find(x => x.ID == id);
            return View(baiviettaivitri);
        }

        
        public ActionResult Camnang(int? page)
        {
            if (page == null) page = 1;

            var topic = db.blog.Where(s => s.TopicID == 2).ToList().OrderByDescending(s => s.ID);
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult LifeStyle(int? page)
        {
            if (page == null) page = 1;

            var topic = db.blog.Where(s => s.TopicID == 3).ToList().OrderByDescending(s => s.ID);
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult KienThucThoiTrang(int? page)
        {
            if (page == null) page = 1;

            var topic = db.blog.Where(s => s.TopicID == 4).ToList().OrderByDescending(s => s.ID);
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Adidas(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 8;
            return View(db.Giays.ToList().Where(m => m.BrandId == 1).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }
        public ActionResult Nike(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 8;
            return View(db.Giays.ToList().Where(m => m.BrandId == 2).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }
        public ActionResult Mlb(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 8;
            return View(db.Giays.ToList().Where(m => m.BrandId == 3).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }
        public ActionResult Converse(int? page)
        {
            if (page == null)
                page = 1;
            int pageSize = 8;
            return View(db.Giays.ToList().Where(m => m.BrandId == 4).OrderByDescending(m => m.Id).ToPagedList(page.Value, pageSize));
        }

    
    }
}