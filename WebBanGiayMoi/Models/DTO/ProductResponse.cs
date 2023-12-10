using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanGiayMoi.Models
{
    public class ProductResponse
    {
        [JsonProperty("san pham goi y")] 
        
        public List<string> SanPhamGoiY { get; set; }
    }
}