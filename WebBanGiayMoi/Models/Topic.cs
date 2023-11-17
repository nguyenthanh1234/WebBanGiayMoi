using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanGiayMoi.Models
{
    [Table("Topic")]
    public class Topic
    {
        public int Id { get; set; }
        [Display(Name ="Chủ đề")]
        public string NameTopic { get; set; }

    }
}