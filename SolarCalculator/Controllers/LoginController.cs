using Microsoft.AspNetCore.Mvc;
using SolarCalculator.Models;
using SolarCalculator.Repositories;
using SolarCalculator.Services;
using SolarCalculator.ViewModel;
using System;
using System.Threading.Tasks;

namespace SolarCalculator.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public LoginController(
            ITokenService tokenService,
            IUserRepository userRepository
        )
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> AuthenticateAsync(
            [FromBody] User model
        )
        {
            var user = await _userRepository
                .GetByUsernameAsync(
                    model.UserName,
                    model.Password
                );

            if (user == null)
            {
                return BadRequest(
                    new
                    {
                        message = "Usuário ou senha inválidos"
                    }
                );
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new
            {
                token,
                user
            });
        }

        [HttpGet]
        [Route("users/{id}")]
        public async Task<ActionResult> GetByIdAsync(
        [FromRoute] Guid id
    )
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Id não informado" });
            }

            var user = await _userRepository.GetByIdAsync(id);

            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterAsync(
            [FromBody] CreateUserViewModel model
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var user = await _userRepository.PostAsync(model);
                return Created($"v1/users/{user.Id}", user);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
