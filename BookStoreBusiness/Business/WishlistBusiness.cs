using BookStoreBusiness.IBusiness;
using BookStoreCommon.Wishlist;
using BookStoreRepository.IRepository;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class WishlistBusiness : IWishlistBusiness
    {
        public readonly IWishlistRepository wishlistRepo;
        public WishlistBusiness(IWishlistRepository wishlistRepo)
        {
            this.wishlistRepo = wishlistRepo;
        }
        NlogOperation nlog = new NlogOperation();
        public Task<int> AddWishlist(Wishlist wishlist, int userId)
        {
            try
            {
                var result = this.wishlistRepo.AddWishlist(wishlist, userId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteWishlist(int UserId, int BookId)
        {
            try
            {
                var result = this.wishlistRepo.DeleteWishlist(UserId, BookId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Wishlist> GetAllWishList(int UserId)
        {
            try
            {
                var result = this.wishlistRepo.GetAllWishList(UserId);
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
