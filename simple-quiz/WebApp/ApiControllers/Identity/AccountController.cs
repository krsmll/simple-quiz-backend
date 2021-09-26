using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App.Identity;
using DTO.App;
using Extensions.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Areas.Identity.Pages.Account;

namespace WebApp.ApiControllers.Identity
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        
        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
            ILogger<RegisterModel> logger, IConfiguration configuration, AppDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }
        
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<JwtResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] Login dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);

            if (appUser == null)
            {
                _logger.LogWarning("WebApi login. User {User} not found!", dto.Email);
                return NotFound(new Message("Incorrect email or password."));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);

            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                );
                _logger.LogInformation("WebApi Login: User {User} logged in successfully", dto.Email);
                var roles = await _userManager.GetRolesAsync(appUser);
                
                return Ok(
                    new JwtResponse
                    {
                        Token = jwt,
                        Username = appUser.UserName,
                        UserId = appUser.Id.ToString(),
                        Roles = roles
                    }
                );
            }

            _logger.LogWarning("WebApi Login Error: User {User} -- Bad password!", dto.Email);
            return NotFound(new Message("Incorrect email or password."));
        }
        
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<JwtResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] Register dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogWarning(" User {User} already registered", dto.Email);
                return BadRequest(new Message("User already registered"));
            }

            Console.WriteLine(dto.Username);
            appUser = new AppUser
            {
                Email = dto.Email,
                UserName = dto.Username
            };
            var result = await _userManager.CreateAsync(appUser, dto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} created a new account with password", appUser.Email);

                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                    _logger.LogInformation("WebApi login. User {User}", dto.Email);

                    var roles = await _userManager.GetRolesAsync(user);
                    
                    return Ok(new JwtResponse
                    {
                        Token = jwt,
                        Username = appUser.UserName,
                        UserId = appUser.Id.ToString(),
                        Roles = roles
                    });
                }

                _logger.LogInformation("User {Email} not found after creation", appUser.Email);
                return BadRequest(new Message("User not found after creation!"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new Message {Messages = errors});
        }
    }
}