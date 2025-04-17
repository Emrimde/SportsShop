using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders(string id);
    }
}
