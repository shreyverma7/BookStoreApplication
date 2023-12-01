using BookStoreBusiness.IBusiness;
using BookStoreCommon.OrderSummary;
using BookStoreRepository.IRepository;
using NlogImplementation;
using System;
using System.Collections.Generic;

namespace BookStoreBusiness.Business
{
    public class OrderSummaryBusiness : IOrderSummaryBusiness
    {
        public readonly IOrderSummaryRepository orderSummaryRepo;
        public OrderSummaryBusiness(IOrderSummaryRepository orderSummaryRepo)
        {
            this.orderSummaryRepo = orderSummaryRepo;
        }
        NlogOperation nlog = new NlogOperation();
        public IEnumerable<SummaryOrder> GetOrderSummary(int UserId, int OrderId)
        {
            try
            {
                var result = this.orderSummaryRepo.GetOrderSummary(UserId, OrderId);
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
