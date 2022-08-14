using mohan_CapstoneProject_SDA.LMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mohan_CapstoneProject_SDA.LMS.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRole(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Medicine).Include(n => n.User).ToListAsync();
                
            if (userRole != "Admin")
            {
                orders = orders.Where(n => n.UserID == userId).ToList();
            }
            return orders;
        } // Vid. 73 & 94 Implement Identity

        public async Task StoreOrder(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserID = userId,
                Email = userEmailAddress,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderitem = new OrderItem()
                {
                    Amount = item.Amount,
                    Price = item.Medicine.Price,
                    MedicineID = item.Medicine.ID,
                    OrderID= order.ID
                };
                await _context.OrderItems.AddAsync(orderitem);   
            } 
            await _context.SaveChangesAsync();
        }// Vid 73
    }
}
