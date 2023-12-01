using BookStoreCommon.Cart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBusiness.IBusiness
{
    public interface ICartBusiness
    {
        public Task<int> AddCart(Carts cart, int userId);
        public bool DeleteCart(int UserId, int BookId);
        public IEnumerable<Carts> GetAllCart(int UserId);
        public bool UpdateCart(Carts obj, int userId);
    }
}
