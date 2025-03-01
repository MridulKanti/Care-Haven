using CareHaven.Data;
using CareHaven.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CareHaven.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> GetFeedbackById(int feedbackId)
        {
            return await _context.Feedbacks.FindAsync(feedbackId);
        }

        public async Task AddFeedback(Feedback feedback, int userId) // Update method signature
        {
            feedback.UserId = userId; // Set the user ID from the method parameter
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateFeedback(int feedbackId, Feedback feedback)
        {
            var existingFeedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (existingFeedback == null)
            {
                return false;
            }

            existingFeedback.FeedbackText = feedback.FeedbackText;
            existingFeedback.Date = feedback.Date;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteFeedback(int feedbackId)
        {
            var feedback = await _context.Feedbacks.FindAsync(feedbackId);
            if (feedback == null)
            {
                return false;
            }

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}