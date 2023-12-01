using BookStoreBusiness.IBusiness;
using BookStoreCommon.Feedback;
using BookStoreRepository.IRepository;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreBusiness.Business
{
    public class FeedbackBusiness : IFeedbackBusiness
    {
        public readonly IFeedbackRepository feedbackRepo;
        public FeedbackBusiness(IFeedbackRepository feedbackRepo)
        {
            this.feedbackRepo = feedbackRepo;
        }
        NlogOperation nlog = new NlogOperation();
        public Task<int> AddFeedback(Feedbacks feedback, int userId)
        {
            try
            {
                var result = this.feedbackRepo.AddFeedback(feedback, userId);
                return result;
            }
            catch (Exception ex)
            {
                nlog.LogWarn(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<Feedbacks> GetAllFeedback(int UserId)
        {
            try
            {
                var result = this.feedbackRepo.GetAllFeedback(UserId);
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
