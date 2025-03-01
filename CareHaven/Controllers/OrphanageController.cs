

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareHaven.Exceptions;
using CareHaven.Models;
using CareHaven.Services;
using CareHaven.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CareHaven.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrphanageController : ControllerBase
    {

        private readonly IOrphanageService _service;
        private readonly IDonationService _dservice;

        public OrphanageController(IOrphanageService orphanageService, IDonationService donationService)
        {
            _service = orphanageService;
            _dservice = donationService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<IEnumerable<OrphanageDTO>>> GetAllOrphanages()
        {
            var orphanages = await _service.GetAllOrphanages();
            var orphanageDTOs = orphanages.Select(o => new OrphanageDTO
            {
                OrphanageId = o.OrphanageId,
                OrphanageName = o.OrphanageName,
                Description = o.Description,
                Founder = o.Founder,
                EstablishmentDate = o.EstablishmentDate,
                Status = o.Status
            }).ToList();

            return Ok(orphanageDTOs);
        }

        [HttpGet("{orphanageId}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<OrphanageDTO>> GetOrphanageById(int orphanageId)
        {
            var orphanage = await _service.GetOrphanageById(orphanageId);

            if (orphanage != null)
            {
                var orphanageDTO = new OrphanageDTO
                {
                    OrphanageId = orphanage.OrphanageId,
                    OrphanageName = orphanage.OrphanageName,
                    Description = orphanage.Description,
                    Founder = orphanage.Founder,
                    EstablishmentDate = orphanage.EstablishmentDate,
                    Status = orphanage.Status
                };
                return Ok(orphanageDTO);
            }
            else
            {
                return NotFound("Cannot find any orphanage");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddOrphanage([FromBody] OrphanageDTO orphanageDTO)
        {
            try
            {
                var orphanage = new Orphanage
                {
                    OrphanageId = orphanageDTO.OrphanageId,
                    OrphanageName = orphanageDTO.OrphanageName,
                    Description = orphanageDTO.Description,
                    Founder = orphanageDTO.Founder,
                    EstablishmentDate = orphanageDTO.EstablishmentDate,
                    Status = orphanageDTO.Status
                };

                var result = await _service.AddOrphanage(orphanage);

                if (result)
                {
                    return Ok("Orphanage added successfully");
                }

                return StatusCode(500, "Failed to add orphanage");
            }
            catch (OrphanageException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{orphanageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateOrphanage(int orphanageId, [FromBody] OrphanageDTO orphanageDTO)
        {
            try
            {
                var orphanage = new Orphanage
                {
                    OrphanageId = orphanageDTO.OrphanageId,
                    OrphanageName = orphanageDTO.OrphanageName,
                    Description = orphanageDTO.Description,
                    Founder = orphanageDTO.Founder,
                    EstablishmentDate = orphanageDTO.EstablishmentDate,
                    Status = orphanageDTO.Status
                };

                var result = await _service.UpdateOrphanage(orphanageId, orphanage);
                if (result)
                {
                    return Ok("Orphanage updated successfully");
                }
                return NotFound("Cannot find any orphanage");
            }
            catch (OrphanageException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{orphanageId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteOrphanage(int orphanageId)
        {
            var res1 = await _dservice.DeleteDonationsByOrphanageId(orphanageId);
            if (!res1) res1 = true;
            try
            {
                var result = await _service.DeleteOrphanage(orphanageId);
                if (result)
                {
                    return Ok(new { message = "Orphanage deleted successfully" });
                }
                return NotFound(new { message = "Cannot find any orphanage" });
            }
            catch (OrphanageException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
