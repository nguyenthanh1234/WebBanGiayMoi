using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;

namespace WebBanGiayMoi.Models
{
    public class CartItem
    {
        //San pham trong gio hang
        public Giay _shopping_product { get; set; }

        //So luong san pham trong gio hang
        public int _shopping_quantity { get; set; }
        public int _shopping_size { get; set; }
    }
    //Giỏ hàng
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items { get { return items; } }
   
        public void Add(Giay _pro, int _quantity, int _size)
        {
            //Neu them 2 lan vao gio hang cung 1 san pham
            var item = items.FirstOrDefault(s => s._shopping_product.Id == _pro.Id);
            int flag = 0;
            if (item == null)
            {
                items.Add(new CartItem
                {
                    _shopping_product = _pro,
                    _shopping_quantity = _quantity,
                    _shopping_size = _size
                });
            }
            else
            {
                foreach(var itemKS in Items.ToList())
                {
                    if (itemKS._shopping_size == _size)
                    {
                        itemKS._shopping_quantity += _quantity;
                        flag = 1;
                    }               
                }
               if(flag != 1) { 
                        items.Add(new CartItem
                        {
                            _shopping_product = _pro,
                            _shopping_quantity = _quantity,
                            _shopping_size = _size
                        });
                }
            }
            //Truoc hop lan dau tien them san pham
                  
        }
        //Cap nhap so luong
        public void Update_Quantity_Shopping(int id, int _quantity, int _size)
        {
            var item = items.Find(s => s._shopping_product.Id == id && s._shopping_size == _size);
            if (_quantity < 1)
            {

            }
            else
            {                
                item._shopping_quantity = _quantity;                 

            }
        }

        //Tinh tong tien
        public double Total_Money()
        {
            var total = items.Sum(s => Math.Round(s._shopping_product.Gia,2) * s._shopping_quantity);
            return (double)total;
        }
        public double Total_MoneyUSD()
        {
            var total = items.Sum(s => Math.Round(s._shopping_product.Gia / 23500,2) * s._shopping_quantity);
            return (double)total;
        }

        //Xoa san pham ra khoi danh sach
        public void Remove_CartItem(int id, int size)
        {

            foreach (var itemKS in Items.ToList())
            {
                if (itemKS._shopping_size == size && itemKS._shopping_product.Id == id)
                {
                    items.RemoveAll(s => s._shopping_product.Id == id && s._shopping_size == size);
                }
            }          
        }

        //Hiển thị số lượng trên icon, tỉnh tổng danh sách
        public int Total_Quantity()
        {
            return items.Sum(s => s._shopping_quantity);
        }
        //Xóa giỏ hàng
        public void ClearCart()
        {
            items.Clear();
        }

    }
}