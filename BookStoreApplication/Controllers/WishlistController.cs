using BookStoreBusiness.IBusiness;
using BookStoreCommon.Wishlist;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.Linq;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        public readonly IWishlistBusiness wishlistBusiness;
        public int userid;
        public WishlistController(IWishlistBusiness wishlistBusiness)
        {
            this.wishlistBusiness = wishlistBusiness;  
        }
        NlogOperation nlog = new NlogOperation();

        [HttpPost]
        [Route("AddWishlist")]
        public ActionResult AddWishlist(Wishlist wishlist)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                
                var result = this.wishlistBusiness.AddWishlist(wishlist,userid);
                if (result != null)
                {
                    nlog.LogInfo("Wishlist Added Successfully");
                    return this.Ok(new { Status = true, Message = "Wishlist Added Successfully", Data = wishlist });
                }
                return this.BadRequest(new { Status = false, Message = "Adding wishlist Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("DeleteWishlist")]
        public ActionResult DeleteWishlist(int BookId)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.wishlistBusiness.DeleteWishlist( userid, BookId);
                if (result)
                {
                    nlog.LogInfo("Wishlist Deleted Successfully");
                    return this.Ok(new { Status = true, Message = "Wishlist Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting wishlist Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllWishList")]
        public ActionResult GetAllWishList()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.wishlistBusiness.GetAllWishList(userid);
                if (result != null)
                {
                    nlog.LogInfo("All Wishlists Found");
                    return this.Ok(new { Status = true, Message = "All Wishlists Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Wishlist Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
