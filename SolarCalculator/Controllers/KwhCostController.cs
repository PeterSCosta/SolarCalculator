using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarCalculator.Repositories;
using SolarCalculator.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarCalculator.Controllers
{
    [ApiController]
    [Route("v1")]
    public class KwhCostController : ControllerBase
    {
        private readonly IKwhCostRepository _kwhCostRepository;

        public KwhCostController(
            IKwhCostRepository kwhCostRepository
        )
        {
            _kwhCostRepository = kwhCostRepository;
        }

        [HttpGet]
        [Route("costs")]
        [Authorize]
        public async Task<ActionResult> GetAsync()
        {
            var costs = await _kwhCostRepository.GetAsync();

            return Ok(costs);
        }

        [HttpGet]
        [Route("costs/{id}")]
        [Authorize]
        public async Task<ActionResult> GetByIdAsync(
            [FromRoute] Guid id
        )
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Id não informado" });
            }

            var cost = await _kwhCostRepository.GetByIdAsync(id);

            return cost == null ? NotFound() : Ok(cost);
        }

        [HttpPost]
        [Route("costs")]
        [Authorize]
        public async Task<ActionResult> PostAsync(
            [FromBody] CreateKwhCostViewModel model
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var costExists = await _kwhCostRepository.GetByStateAsync(model.State);
            if (costExists != null)
            {
                return BadRequest(new { message = "Estado já cadastrado." });
            }

            try
            {
                var cost = await _kwhCostRepository.PostAsync(model);
                return Created($"v1/costs/{cost.Id}", cost);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("costs/{id}")]
        [Authorize]
        public async Task<ActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] EditKwhCostViewModel model
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Id não informado" });
            }

            try
            {
                var cost = await _kwhCostRepository.PutAsync(id, model);
                return Ok(cost);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("costs/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteAsync(
            [FromRoute] Guid id
        )
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Id não informado" });
            }

            try
            {
                await _kwhCostRepository.DeleteAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}