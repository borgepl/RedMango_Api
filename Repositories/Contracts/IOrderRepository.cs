using DataAccess.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IOrderRepository : IGenericRepository<OrderHeader>
    {
        Task AddOrderDetails(IList<OrderDetail> orderDetails);
    }
}
