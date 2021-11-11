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
    public class GeneratorCostController : ControllerBase
    {
        private readonly IGeneratorCostRepository _generatorCostRepository;

        public GeneratorCostController(
            IGeneratorCostRepository generatorCostRepository
        )
        {
            _generatorCostRepository = generatorCostRepository;
        }

        [HttpGet]
        [Route("generatorcosts")]
        [Authorize]
        public async Task<ActionResult> GetAsync()
        {
            var generatorCost = await _generatorCostRepository.GetAsync();

            return Ok(generatorCost);
        }

        [HttpGet]
        [Route("generatorcosts/{id}")]
        [Authorize]
        public async Task<ActionResult> GetByIdAsync(
            [FromRoute] Guid id
        )
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Id não informado" });
            }

            var generatorCost = await _generatorCostRepository.GetByIdAsync(id);

            return generatorCost == null ? NotFound() : Ok(generatorCost);
        }

        [HttpPost]
        [Route("generatorcosts")]
        [Authorize]
        public async Task<ActionResult> PostAsync(
            [FromBody] CreateGeneratorCostViewModel model
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var generatorCostExists = await _generatorCostRepository.GetAsync();
            if (generatorCostExists.Count > 0)
            {
                return BadRequest(new { message = "Custo de gerador já cadastrado." });
            }

            try
            {
                var generatorCost = await _generatorCostRepository.PostAsync(model);
                return Created($"v1/generatorcosts/{generatorCost.Id}", generatorCost);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("generatorcosts/{id}")]
        [Authorize]
        public async Task<ActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] CreateGeneratorCostViewModel model
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
                var generatorCost = await _generatorCostRepository.PutAsync(id, model);
                return Ok(generatorCost);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("generatorcosts/{id}")]
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
                await _generatorCostRepository.DeleteAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}