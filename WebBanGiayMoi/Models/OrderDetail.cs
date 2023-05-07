using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanGiayMoi.Models;

namespace WebBanGiayMoi.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public Giay Giay { get; set; }
        [Required]
        public int GiayId { get; set; }

        public Order Order { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Display(Name = "Giá mặt hàng")]
        public double UnitPriceSale { get; set; }
        [Display(Name = "Size")]
        public int Size { get; set; }

        [Display(Name = "Số lượng")]
        public int QuantitySale { get; set; }

    }
}