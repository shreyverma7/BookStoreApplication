using BookStoreBusiness.IBusiness;
using BookStoreCommon.Cart;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.Linq;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public readonly ICartBusiness cartBusiness;
        public int userid;
        public CartController(ICartBusiness cartBusiness)
        {
            this.cartBusiness = cartBusiness;
        }
        NlogOperation nlog = new NlogOperation();

        [HttpPost]
        [Route("AddtoCart")]
        public ActionResult AddCart(Carts cart)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                
                var result = this.cartBusiness.AddCart(cart,userid);
                if (result != null)
                {
                    nlog.LogInfo("Cart Added Successfully");
                    return this.Ok(new { Status = true, Message = "Cart Added Successfully", Data = cart });
                }
                return this.BadRequest(new { Status = false, Message = "Adding cart Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("DeletefromCart")]
        public ActionResult DeleteCart(int BookId)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                bool result = this.cartBusiness.DeleteCart( userid, BookId);
                if (result)
                {
                    nlog.LogInfo("Cart Deleted Successfully");
                    return this.Ok(new { Status = true, Message = "Cart Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Cart Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllCart")]
        public ActionResult GetAllCart()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.cartBusiness.GetAllCart(userid);
                if (result != null)
                {
                    nlog.LogInfo("All Carts Found");
                    return this.Ok(new { Status = true, Message = "All Carts Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Carts Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateCart")]
        public ActionResult UpdateCart(Carts obj)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.cartBusiness.UpdateCart(obj, userid);
                if (result)
                {
                    nlog.LogInfo("Cart Updated Successfully");
                    return this.Ok(new { Status = true, Message = "Cart Updated Successfully", Data = obj });
                }
                return this.BadRequest(new { Status = false, Message = "Updating Cart Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
