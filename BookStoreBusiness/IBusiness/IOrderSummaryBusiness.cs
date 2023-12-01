using BookStoreCommon.OrderSummary;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreBusiness.IBusiness
{
    public interface IOrderSummaryBusiness
    {
        public IEnumerable<SummaryOrder> GetOrderSummary(int UserId, int OrderId);
    }
}
