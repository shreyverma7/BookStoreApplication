using BookStoreBusiness.IBusiness;
using BookStoreCommon.CustomerDetail;
using BookStoreRepository.IRepository;
using NlogImplementation;
using System;
using System.Collections.Generic;

namespace BookStoreBusiness.Business
{
    public class CustomerDetailBusiness : ICustomerDetailBusiness
    {
        public readonly ICustomerDetailRepository customerDetailRepo;
        public CustomerDetailBusiness(ICustomerDetailRepository customerDetailRepo)
        {
            this.customerDetailRepo = customerDetailRepo;
        }
        NlogOperation nlog = new NlogOperation();
        public CustomerDetails AddAddress(CustomerDetails customerDetails, int userId)
        {
            try
            {
                var result = this.customerDetailRepo.AddAddress(customerDetails, userId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteAddress(int CustomerId, int UserId)
        {
            try
            {
                var result = this.customerDetailRepo.DeleteAddress(CustomerId, UserId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<CustomerDetails> GetAllAddress(int UserId)
        {
            try
            {
                var result = this.customerDetailRepo.GetAllAddress(UserId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateAddress(CustomerDetails obj, int userId)
        {
            try
            {
                var result = this.customerDetailRepo.UpdateAddress(obj, userId);
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
