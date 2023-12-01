using BookStoreCommon.OrderSummary;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepository.IRepository
{
    public interface IOrderSummaryRepository
    {
        public IEnumerable<SummaryOrder> GetOrderSummary(int UserId, int OrderId);
    }
}
