﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.Interfaces.IOrder
{
    public interface IOrderAdderService
    {
        Task<Order> AddOrder(Order order);
    }
}
