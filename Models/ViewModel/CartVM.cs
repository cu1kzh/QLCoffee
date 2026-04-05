using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCoffee.Models.ViewModel
{
    public class CartVM
    {
        public List<CartItem> CartItems { get; set; }
        public int TotalAmount => CartItems?.Sum(item => item.TotalPrice) ?? 0;
    }
}