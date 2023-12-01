using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface IOrderPlacedRepository
    {
        public Task<int> PlaceOrder(int UserId, int CartId, int CustomerId);
    }
}
