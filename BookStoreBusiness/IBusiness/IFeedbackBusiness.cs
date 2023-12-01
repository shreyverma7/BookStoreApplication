using BookStoreCommon.Feedback;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBusiness.IBusiness
{
    public interface IFeedbackBusiness
    {
        public Task<int> AddFeedback(Feedbacks feedback, int userId);
        public IEnumerable<Feedbacks> GetAllFeedback(int UserId);
    }
}
