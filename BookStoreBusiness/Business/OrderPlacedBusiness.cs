using BookStoreBusiness.IBusiness;
using BookStoreRepository.IRepository;
using NlogImplementation;
using System;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class OrderPlacedBusiness : IOrderPlacedBusiness
    {
        public readonly IOrderPlacedRepository orderPlacedRepo;
        public OrderPlacedBusiness(IOrderPlacedRepository orderPlacedRepo)
        {
            this.orderPlacedRepo = orderPlacedRepo;
        }
        NlogOperation nlog = new NlogOperation();
        public Task<int> PlaceOrder(int UserId, int CartId, int CustomerId)
        {
            try
            {
                var result = this.orderPlacedRepo.PlaceOrder(UserId, CartId, CustomerId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
