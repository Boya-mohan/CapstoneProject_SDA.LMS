using mohan_CapstoneProject_SDA.LMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mohan_CapstoneProject_SDA.LMS.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; set; }
        public string ShoppingCartID2 { get; set; } // Local ShoppingCartID
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

       public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider services) // Initiate of Cart and Session
        {                                                                     //  ? if not null
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
                                                    //  ?? if null
            string CartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", CartId);

            return new ShoppingCart(context) { ShoppingCartID2 = CartId };
        }

        public void AddItemToCart(Medicine medicine)
        {
            var shoppingCartItem = _context.shoppingCartItems.FirstOrDefault(n => n.Medicine.ID == medicine.ID
            && n.ShoppingCartID == ShoppingCartID2);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {   ShoppingCartID = ShoppingCartID2,
                    Medicine = medicine,
                    Amount = 1};
                _context.shoppingCartItems.Add(shoppingCartItem);
            } 
            
            else { shoppingCartItem.Amount++; }         

            _context.SaveChanges();                     // Vid 65
        }

        public void DeleteItemFromCart(Medicine medicine)
        {
            var shoppingCartItem = _context.shoppingCartItems.FirstOrDefault(n => n.Medicine.ID == medicine.ID
            && n.ShoppingCartID == ShoppingCartID2);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1) { shoppingCartItem.Amount--; }
            
            else { _context.shoppingCartItems.Remove(shoppingCartItem);}
            }
            _context.SaveChanges();                     // Vid 66
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.shoppingCartItems.Where(n => n.ShoppingCartID == ShoppingCartID2)
                .Include(n => n.Medicine).ToList());    // Vid 64
        }

        public double GetShoppingCartTotal()
        {
            var Total = _context.shoppingCartItems.Where(n => n.ShoppingCartID == ShoppingCartID2)
                .Select(n => n.Medicine.Price * n.Amount).Sum();
            return Total;                               // Vid 64
        } 

        public async Task ClearShoppingCart()
        {
            var items = await _context.shoppingCartItems.Where(n => n.ShoppingCartID == ShoppingCartID2).ToListAsync();
            _context.shoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }                                               // Vid 74
    }
}
