using BookStoreBusiness.IBusiness;
using BookStoreCommon.CustomerDetail;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.Linq;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailController : ControllerBase
    {
        public readonly ICustomerDetailBusiness customerDetailBusiness;
        public int userid;
        public CustomerDetailController(ICustomerDetailBusiness customerDetailBusiness)
        {
            this.customerDetailBusiness = customerDetailBusiness;
        }
        NlogOperation nlog = new NlogOperation();

        [HttpPost]
        [Route("AddCustomerDetails")]
        public ActionResult AddAddress(CustomerDetails customerDetails)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.customerDetailBusiness.AddAddress(customerDetails, userid);
                if (result != null)
                {
                    nlog.LogInfo("Address Added Successfully");
                    return this.Ok(new { Status = true, Message = "Address Added Successfully", Data = customerDetails });
                }
                return this.BadRequest(new { Status = false, Message = "Adding address Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("DeleteCustomerDetails")]
        public ActionResult DeleteAddress(int CustomerId)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.customerDetailBusiness.DeleteAddress(CustomerId, userid);
                if (result)
                {
                    nlog.LogInfo("Address Deleted Successfully");
                    return this.Ok(new { Status = true, Message = "Address Deleted Successfully" });
                }
                return this.BadRequest(new { Status = false, Message = "Deleting Address Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllCustomerDetails")]
        public ActionResult GetAllAddress()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.customerDetailBusiness.GetAllAddress(userid);
                if (result != null)
                {
                    nlog.LogInfo("All Address Found");
                    return this.Ok(new { Status = true, Message = "All Address Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Address Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateCustomerDetails")]
        public ActionResult UpdateAddress(CustomerDetails obj)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.customerDetailBusiness.UpdateAddress(obj, userid);
                if (result)
                {
                    nlog.LogInfo("Address Updated Successfully");
                    return this.Ok(new { Status = true, Message = "Address Updated Successfully", Data = obj });
                }
                return this.BadRequest(new { Status = false, Message = "Updating Address Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
