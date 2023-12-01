using BookStoreBusiness.IBusiness;
using BookStoreCommon.User;
using BookStoreRepository.IRepository;
using NlogImplementation;
using System;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class UserBusiness : IUserBusiness
    {
        public readonly IUserRepository userRepo;
        public UserBusiness(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }
        NlogOperation nlog = new NlogOperation();
        public Task<int> UserRegistration(UserRegister obj)
        {
            try
            {
                var result = this.userRepo.UserRegistration(obj);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public string UserLogin(string email, string password)
        {
            try
            {
                var result = this.userRepo.UserLogin(email, password);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                var result = this.userRepo.ForgetPassword(email);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public UserRegister ResetPassword(string email, string newpassword, string confirmpassword)
        {
            try
            {
                var result = this.userRepo.ResetPassword(email, newpassword, confirmpassword);
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
