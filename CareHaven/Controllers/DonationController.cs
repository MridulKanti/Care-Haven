
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
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;
        private readonly ILogger<DonationController> _logger;

        public DonationController(IDonationService donationService, ILogger<DonationController> logger)
        {
            _donationService = donationService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<DonationDTO>>> GetAllDonations()
        {
            try
            {
                var donations = await _donationService.GetAllDonations();
                var donationDTOs = donations.Select(d => new DonationDTO
                {
                    DonationId = d.DonationId,
                    OrphanageId = d.OrphanageId,
                    Amount = d.Amount,
                    DonationDate = d.DonationDate,
                    UserId = d.UserId
                }).ToList();

                return Ok(donationDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all donations");
                return StatusCode(500, "Failed to get all donations");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AddDonation([FromBody] DonationDTO donationDTO)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); // Get user ID from JWT token

                var donation = new Donation
                {
                    DonationId = donationDTO.DonationId,
                    OrphanageId = donationDTO.OrphanageId,
                    Amount = donationDTO.Amount,
                    DonationDate = donationDTO.DonationDate
                };

                var result = await _donationService.AddDonation(donation, userId); // Pass user ID to the service method
                if (result.Item1)
                {
                    return Ok(new { message = result.Item2 });
                }
                return BadRequest(new { message = result.Item2 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add donation");
                return StatusCode(500, "Failed to add donation");
            }
        }

        [HttpGet("User/{userId}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<DonationDTO>>> GetDonationsByUserId(int userId)
        {
            try
            {
                var donations = await _donationService.GetDonationsByUserId(userId);
                var donationDTOs = donations.Select(d => new DonationDTO
                {
                    DonationId = d.DonationId,
                    OrphanageId = d.OrphanageId,
                    Amount = d.Amount,
                    DonationDate = d.DonationDate,
                    UserId = d.UserId
                }).ToList();

                return Ok(donationDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get donation by userId");
                return StatusCode(500, "Failed to get donation by userId");
            }
        }

        [HttpGet("orphanages/{orphanageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<DonationDTO>>> GetDonationsByOrphanageId(int orphanageId)
        {
            try
            {
                var donations = await _donationService.GetDonationsByOrphanageId(orphanageId);
                var donationDTOs = donations.Select(d => new DonationDTO
                {
                    DonationId = d.DonationId,
                    OrphanageId = d.OrphanageId,
                    Amount = d.Amount,
                    DonationDate = d.DonationDate,
                    UserId = d.UserId
                }).ToList();

                return Ok(donationDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get donation by orphanageId");
                return StatusCode(500, "Failed to get donation by orphanageId");
            }
        }
    }
}
