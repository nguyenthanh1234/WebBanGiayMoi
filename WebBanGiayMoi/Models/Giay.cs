using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using WebBanGiayMoi.Models;

namespace WebBanGiayMoi.Models
{
    public class Giay
    {
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Display(Name = "Mô tả ngắn")]
        public string Description { get; set; }
        [Display(Name = "Giá")]
        public int Gia { get; set; }
        //[Display(Name = "Số lượng")]
        //public int SoLuong { get; set; }
        [Display(Name = "Ảnh đại diện")]
        public string Hinh { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        public Category Category { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Brand Brand { get; set; }
        public int BrandId { get; set; }
        
        public string Size { get; set; }

        public static List<Giay> GetAll(string searchKey)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            searchKey = searchKey + "";
            return context.Giays.Where(p => p.Name.Contains(searchKey)).ToList();
        }
    }
}