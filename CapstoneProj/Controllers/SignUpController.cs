using CapstoneProj.Models;
using CapstoneProj.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CapstoneProj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _userRepository;

        public SignupController(IConfiguration configuration, IUserRepo userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                null,
                expires: DateTime.Now.AddMinutes(30), // Set the token expiration as per your requirement
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Signup(Users user)
        {
            IActionResult response = BadRequest("Signup failed");

            // Check if the username is already taken
            var existingUser = _userRepository.GetUser(user.Username, user.Password);
            if (existingUser != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Username is already taken");
            }

            // Add the user to the repository
            _userRepository.AddUser(user);

            // Generate token for successful signup
            var token = GenerateToken(user);
            return Ok(new { token });
        }
    }
}
