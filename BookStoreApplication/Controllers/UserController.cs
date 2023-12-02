using BookStoreBusiness.IBusiness;
using BookStoreCommon.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserBusiness userBusiness;
        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }
        NlogOperation nlog = new NlogOperation();

        [HttpPost]
        [Route("Registration")]
        public async Task<ActionResult> UserRegistration(UserRegister userRegister)
        {
            try
            {
                var result = await this.userBusiness.UserRegistration(userRegister);
                if (result != 0)
                {
                    nlog.LogInfo("User Registered Successfully");
                    return this.Ok(new { Status = true, Message = "User Registered Successfully", Data = userRegister });
                }
                return this.BadRequest(new { Status = false, Message = "User Registration Unsuccessful", Data = String.Empty });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult UserLogin(string email, string password)
        {
            try
            {
                var result = this.userBusiness.UserLogin(email, password);
                if (result != null)
                {
                    var tokenhandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenhandler.ReadJwtToken(result);
                    var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id");
                    string Id = id.Value;
                    nlog.LogInfo("User Logged In Successfully");
                    return this.Ok(new { Status = true, Message = "User Logged In Successfully", Data = result, id = Id });
                }
                return this.BadRequest(new { Status = false, Message = "User Login Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var result = this.userBusiness.ForgetPassword(email);
                if (result != null)
                {
                    nlog.LogInfo("Reset Email Send");
                    return this.Ok(new { Status = true, Message = "Reset Email Send" });
                }
                return this.BadRequest(new { Status = false, Message = "Reset UnSuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { StatusCode = this.BadRequest(), Status = false, Message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public ActionResult ResetPassword(string newpassword, string confirmpassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = this.userBusiness.ResetPassword(email,newpassword, confirmpassword);
                if (result != null)
                {
                    nlog.LogInfo("User Password Reset Successful");
                    return this.Ok(new { Status = true, Message = "User Password Reset Successful", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "User Password Reset Unsuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}
