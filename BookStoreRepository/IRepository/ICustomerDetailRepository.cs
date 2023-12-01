using BookStoreCommon.CustomerDetail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepository.IRepository
{
    public interface ICustomerDetailRepository
    {
        public  CustomerDetails AddAddress(CustomerDetails customerDetails, int userId);
        public bool DeleteAddress(int CustomerId, int UserId);
        public IEnumerable<CustomerDetails> GetAllAddress(int UserId);
        public bool UpdateAddress(CustomerDetails obj, int userId);

    }
}
