using BookStoreBusiness.IBusiness;
using BookStoreCommon.Feedback;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.Linq;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        public readonly IFeedbackBusiness feedbackBusiness;
        public int userid;
        public FeedbackController(IFeedbackBusiness feedbackBusiness)
        {
            this.feedbackBusiness = feedbackBusiness;
        }
        NlogOperation nlog = new NlogOperation();

        [HttpPost]
        [Route("AddFeedback")]
        public ActionResult AddFeedback(Feedbacks feedback)
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);

                var result = this.feedbackBusiness.AddFeedback(feedback, userid);
                if (result != null)
                {
                    nlog.LogInfo("Feedback Added Successfully");
                    return this.Ok(new { Status = true, Message = "Feedback Added Successfully", Data = feedback });
                }
                return this.BadRequest(new { Status = false, Message = "Adding Feedback Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllFeedback")]
        public ActionResult GetAllFeedback()
        {
            try
            {
                int userid = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "Id").Value);
                var result = this.feedbackBusiness.GetAllFeedback(userid);
                if (result != null)
                {
                    nlog.LogInfo("All Feedbacks Found");
                    return this.Ok(new { Status = true, Message = "All Feedbacks Found", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "No Feedbacks Found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
