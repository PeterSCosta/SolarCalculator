using Microsoft.AspNetCore.Mvc;
using SolarCalculator.Repositories;
using SolarCalculator.ViewModel;
using System;
using System.Threading.Tasks;

namespace SolarCalculator.Controllers
{
    [ApiController]
    [Route("v1")]
    public class SimulationController : ControllerBase
    {
        private readonly ISimulationRepository _simulationRepository;

        public SimulationController(
            ISimulationRepository simulationRepository
        )
        {
            _simulationRepository = simulationRepository;
        }

        [HttpGet]
        [Route("simulations")]
        public async Task<ActionResult> GetAsync()
        {
            var simulations = await _simulationRepository.GetAsync();

            return Ok(simulations);
        }

        [HttpGet]
        [Route("simulations/{id}")]
        public async Task<ActionResult> GetByIdAsync(
            [FromRoute] Guid id
        )
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Id não informado" });
            }

            var simulation = await _simulationRepository.GetByIdAsync(id);

            return simulation == null ? NotFound() : Ok(simulation);
        }

        [HttpPost]
        [Route("simulations")]
        public async Task<ActionResult> PostAsync(
            [FromBody] CreateSimulationViewModel model
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var simulation = await _simulationRepository.PostAsync(model);
                return Created($"v1/simulations/{simulation.Id}", simulation);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
