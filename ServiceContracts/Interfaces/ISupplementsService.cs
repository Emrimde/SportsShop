using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace ServiceContracts.Interfaces
{
    public interface ISupplementsService
    {
        Task<List<Supplement>> GetAllSupplements();
    }
}
