using QLCoffee.Models.ViewModel;
using QLCoffee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCoffee.Controllers.FactoryMethodHOADON
{
    public interface IOrderFactory
    {
        HOADON CreateOrder(string newOrderId, string login, CheckoutVM model, Cart cart);
    }
}
