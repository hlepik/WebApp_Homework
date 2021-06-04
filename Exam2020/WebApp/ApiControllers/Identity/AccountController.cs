using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Message = Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging.Message;

namespace WebApp.ApiControllers.Identity
{
    /// <summary>
    /// Api endpoint for registering new user and user log-in (jwt token generation)
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<Domain.App.Identity.AppUser> _signInManager;
        private readonly UserManager<Domain.App.Identity.AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="logger"></param>
        public AccountController(SignInManager<Domain.App.Identity.AppUser> signInManager, UserManager<Domain.App.Identity.AppUser> userManager,
            ILogger<AccountController> logger, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Endpoint for user log-in (jwt generation)
        /// </summary>
        /// <param name="dto">login data</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(JwtResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Message), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<JwtResponse>> Login([FromBody] Login dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            // TODO: wait a random time here to fool timing attacks
            if (appUser == null)
            {
                _logger.LogWarning("WebApi login failed. User {User} not found", dto.Email);
                return NotFound("User/Password problem!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = Extensions.Base.IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                _logger.LogInformation("WebApi login. User {User}", dto.Email);
                return Ok(new JwtResponse()
                {
                    Token = jwt,
                    Firstname = appUser.Firstname,
                    Lastname = appUser.Lastname,
                });
            }

            _logger.LogWarning("WebApi login failed. User {User} - bad password", dto.Email);
            return NotFound("User/Password problem!");
        }


        /// <summary>
        /// Endpoint for user registration and immediate log-in (jwt generation)
        /// </summary>
        /// <param name="dto">user data</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JwtResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Message))]
        public async Task<IActionResult> Register([FromBody] Register dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogWarning(" User {User} already registered", dto.Email);
                return NotFound("User already registered!");
            }

            appUser = new Domain.App.Identity.AppUser()
            {
                Email = dto.Email,
                UserName = dto.Email,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
            };
            var result = await _userManager.CreateAsync(appUser, dto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {Email} created a new account with password", appUser.Email);

                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = Extensions.Base.IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                    _logger.LogInformation("WebApi login. User {User}", dto.Email);
                    return Ok(new JwtResponse()
                    {
                        Token = jwt,
                        Firstname = appUser.Firstname,
                        Lastname = appUser.Lastname,
                    });

                }
                else
                {
                    _logger.LogInformation("User {Email} not found after creation", appUser.Email);
                    return BadRequest("User not found after creation!");
                }
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest();
        }

    }

}