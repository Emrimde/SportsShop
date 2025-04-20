using Entities.DatabaseContext;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly SportsShopDbContext _context;
        public OrderService(SportsShopDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllOrders(string id)
        {
            return await _context.Orders.Include(item => item.User).Include(o => o.CartItems)              
            .ThenInclude(ci => ci.Product).Where(item => item.UserId.ToString() == id && item.IsActive).ToListAsync();
        }
    }
}
