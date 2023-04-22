using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebBanGiayMoi.Models;

namespace WebBanGiayMoi.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required]
        public string Descriptions { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required]
        public string PhoneKH { get; set; }

        [Display(Name = "Ngày đặt")]
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}