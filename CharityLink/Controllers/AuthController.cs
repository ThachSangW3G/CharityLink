using CharityLink.Dtos.Authentications;
using CharityLink.Interfaces;
using CharityLink.Models;
using CharityLink.Repositories;
using CharityLink.Sevices;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

namespace CharityLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;

        public AuthController(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already in use.");
            }

            //var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password); 

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
            };

            await _userRepository.CreateAsync(user);

            return Ok("User registered successfully.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var user = await _userRepository.LoginByEmailAndPassword(dto.Email, dto.Password);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = _authService.GenerateJwtToken(user);

            return Ok(new { Token = token });
        }
    }

  
}
