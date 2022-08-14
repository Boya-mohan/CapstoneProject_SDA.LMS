using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using malshetwi_CapstoneProject_SDA.LMS.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace mohan_CapstoneProject_SDA.LMS.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            return View (items.Count);
        }

    }                                                // Vid.72
}
