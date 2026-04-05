using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class Cart//Để quản lý danh sách các sản phẩm trong giỏ
    {
        public QuanLyQuanCoffeeEntities db = new QuanLyQuanCoffeeEntities();

        public List<CartItem> Items { get; set; } = new List<CartItem>();
        

        
        public void AddItem(string productId, string ImgPro, string TenSP, int GiaSP, int quantity )
        {
            var existingItem = Items.FirstOrDefault(i => i.MaSP == productId);
            if(existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {

                Items.Add(new CartItem { MaSP = productId, ProImage = ImgPro, TenSP = TenSP, UnitPrice = GiaSP, Quantity = quantity });
            }
           
           
        }
        //Delete Product from Cart
        public void RemoveItem(string productId)
        {
            Items.RemoveAll( i => i.MaSP == productId);
                
            
        }
        //Total Price of items into Cart
        public int TotalValue()
        {
            return Items.Sum(i => i.TotalPrice);
        }
        //
        public void Clear()
        {
            Items.Clear();
        }

        public void UpdateQuantity(string productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.MaSP == productId);
            if (item != null)
            {
                if(quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    RemoveItem(productId);
                }
            }
        }

        
    }
}