using BookStoreBusiness.IBusiness;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.Linq;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPlacedController : ControllerBase
    {
        public readonly IOrderPlacedBusiness orderPlacedBusiness;
       
        public OrderPlacedController(IOrderPlacedBusiness orderPlacedBusiness)
        {
            this.orderPlacedBusiness = orderPlacedBusiness;
        }
        NlogOperation nlog = new NlogOperation();

        [HttpPost]
        [Route("PlaceOrder")]
        public ActionResult PlaceOrder(int CartId, int CustomerId)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.orderPlacedBusiness.PlaceOrder(userid,CartId, CustomerId);
                if (result != null)
                {
                    nlog.LogInfo("Order Placed Successfully");
                    return this.Ok(new { Status = true, Message = "Order Placed Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Placing Order Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
    }
}
