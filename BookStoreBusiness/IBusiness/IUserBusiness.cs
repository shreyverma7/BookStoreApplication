using BookStoreCommon.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.IBusiness
{
    public interface IUserBusiness
    {
        public  Task<int> UserRegistration(UserRegister obj);
        public string UserLogin(string email, string password);
        public string ForgetPassword(string email);
        public UserRegister ResetPassword(string email, string newpassword, string confirmpassword);
    }
}
