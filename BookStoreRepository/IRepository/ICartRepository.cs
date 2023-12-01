using BookStoreCommon.Cart;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface ICartRepository
    {
        public Task<int> AddCart(Carts cart, int userId);
        public bool DeleteCart(int UserId, int BookId);
        public IEnumerable<Carts> GetAllCart(int UserId);
        public bool UpdateCart(Carts obj, int userId);
    }
}
