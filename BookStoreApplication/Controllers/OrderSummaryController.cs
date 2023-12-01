using BookStoreBusiness.IBusiness;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.Linq;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSummaryController : ControllerBase
    {
        public readonly IOrderSummaryBusiness orderSummaryBusiness;
        public int userid;
        public OrderSummaryController(IOrderSummaryBusiness orderSummaryBusiness)
        {
            this.orderSummaryBusiness = orderSummaryBusiness;
        }
        NlogOperation nlog = new NlogOperation();

        [HttpGet]
        [Route("GetOrderSummary")]
        public ActionResult GetOrderSummary(int OrderId)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.orderSummaryBusiness.GetOrderSummary(userid, OrderId);
                if (result != null)
                {
                    nlog.LogInfo("All Order Summary Found");
                    return this.Ok(new { Status = true, Message = "All Order Summary Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Order Summary Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
