using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanGiayMoi.Models
{
    public class News
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }   
        public string ShortContent { get; set; }
        public string Content { get; set; } 
        public Topic Topic { get; set; }
        [Required]
        public int TopicID { get; set; }
    }
}