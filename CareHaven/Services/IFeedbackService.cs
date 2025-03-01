using CareHaven.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareHaven.Services
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacks();
        Task<Feedback> GetFeedbackById(int feedbackId);
        Task AddFeedback(Feedback feedback, int userId); // Update method signature
        Task<bool> UpdateFeedback(int feedbackId, Feedback feedback);
        Task<bool> DeleteFeedback(int feedbackId);
    }
}
