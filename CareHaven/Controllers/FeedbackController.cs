using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareHaven.Services;
using CareHaven.Models;
using CareHaven.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CareHaven.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(IFeedbackService feedbackService, ILogger<FeedbackController> logger)
        {
            _feedbackService = feedbackService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FeedbackDTO>>> GetAllFeedbacks()
        {
            try
            {
                var feedbacks = await _feedbackService.GetAllFeedbacks();
                var feedbackDTOs = feedbacks.Select(f => new FeedbackDTO
                {
                    FeedbackId = f.FeedbackId,
                    UserId = f.UserId,
                    FeedbackText = f.FeedbackText,
                    Date = f.Date
                }).ToList();

                return Ok(feedbackDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all feedbacks");
                return StatusCode(500, "Failed to get all feedbacks");
            }
        }

        [HttpGet("{feedbackId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<FeedbackDTO>> GetFeedbackById(int feedbackId)
        {
            try
            {
                var feedback = await _feedbackService.GetFeedbackById(feedbackId);
                if (feedback == null)
                {
                    return NotFound("Feedback not found");
                }

                var feedbackDTO = new FeedbackDTO
                {
                    FeedbackId = feedback.FeedbackId,
                    UserId = feedback.UserId,
                    FeedbackText = feedback.FeedbackText,
                    Date = feedback.Date
                };

                return Ok(feedbackDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get feedback by id");
                return StatusCode(500, "Failed to get feedback by id");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AddFeedback([FromBody] FeedbackDTO feedbackDTO)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); // Get user ID from JWT token

                var feedback = new Feedback
                {
                    FeedbackId = feedbackDTO.FeedbackId,
                    FeedbackText = feedbackDTO.FeedbackText,
                    Date = feedbackDTO.Date
                };

                await _feedbackService.AddFeedback(feedback, userId); // Pass user ID to the service method
                return Ok(new { message = "Feedback added successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add feedback");
                return StatusCode(500, "Failed to add feedback");
            }
        }

        [HttpPut("{feedbackId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> UpdateFeedback(int feedbackId, [FromBody] FeedbackDTO feedbackDTO)
        {
            try
            {
                var feedback = new Feedback
                {
                    FeedbackId = feedbackDTO.FeedbackId,
                    UserId = feedbackDTO.UserId,
                    FeedbackText = feedbackDTO.FeedbackText,
                    Date = feedbackDTO.Date
                };

                var result = await _feedbackService.UpdateFeedback(feedbackId, feedback);
                if (result)
                {
                    return Ok("Feedback updated successfully");
                }
                return NotFound("Feedback not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update feedback");
                return StatusCode(500, "Failed to update feedback");
            }
        }

        [HttpDelete("{feedbackId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteFeedback(int feedbackId)
        {
            try
            {
                var result = await _feedbackService.DeleteFeedback(feedbackId);
                if (result)
                {
                    return Ok(new { message = "Feedback deleted successfully" });
                }
                return NotFound(new { message = "Feedback not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete feedback");
                return StatusCode(500, "Failed to delete feedback");
            }
        }
    }
}
