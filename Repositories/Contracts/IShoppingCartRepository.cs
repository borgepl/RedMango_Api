using DataAccess.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
    {
        Task AddCartItem(CartItem item);
        Task UpdateCartItem(CartItem item);
        Task DeleteCartItem(CartItem item);
    }
}
