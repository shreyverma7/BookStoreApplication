using BookStoreBusiness.IBusiness;
using BookStoreCommon.Cart;
using BookStoreRepository.IRepository;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class CartBusiness : ICartBusiness
    {
        public readonly ICartRepository cartRepo;
        public CartBusiness(ICartRepository cartRepo)
        {
            this.cartRepo = cartRepo;
        }
        NlogOperation nlog = new NlogOperation();
        public Task<int> AddCart(Carts cart, int userId)
        {
            try
            {
                var result = this.cartRepo.AddCart(cart, userId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteCart(int UserId, int BookId)
        {
            try
            {
                var result = this.cartRepo.DeleteCart(UserId, BookId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Carts> GetAllCart(int UserId)
        {
            try
            {
                var result = this.cartRepo.GetAllCart(UserId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateCart(Carts obj, int userId)
        {
            try
            {
                var result = this.cartRepo.UpdateCart(obj, userId);
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
