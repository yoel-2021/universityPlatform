using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using universityPlatform.dataAccess;
using universityPlatform.Helpers;
using universityPlatform.Models.dataAccess;
using universityPlatform.TokenCreation;

namespace universityPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<LoginController> _logger;
        

        public LoginController(UniversityContext context, JwtSettings jwtSettings, ILogger<LoginController> logger)
        {
            _jwtSettings = jwtSettings;
            _logger = logger;
            _context = context;

        }
        private readonly UniversityContext _context;
        [HttpPost]
        public IActionResult GetToken(UsersLogin userlogins)
        {
            try
            {
                var Token = new UserTokens();


                //Search a user in context with LinQ

                var searchUser = (from user in _context.User
                                  where user.name == userlogins.UserName && user.password == userlogins.Password && user.email == userlogins.Email
                                  select user).FirstOrDefault();

                Console.WriteLine("User found", searchUser);
                if (searchUser != null)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = searchUser.name,
                        Email = searchUser.email,
                        role = searchUser.role,
                        Id = searchUser.id,
                        GuidId = Guid.NewGuid(),
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }

                return Ok(Token);

            }
            catch (Exception ex)
            {
                throw new Exception("Get Token Error", ex);
            }
            
        }
        
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public IActionResult GetUserList()
        {
            _logger.LogTrace($"{nameof(LoginController)} - {nameof(GetUserList)} - Trace Level Log");
            _logger.LogDebug($"{nameof(LoginController)} - {nameof(GetUserList)} - Debug Level Log ");
            _logger.LogWarning($"{nameof(LoginController)} - {nameof(GetUserList)} - Warning Level Log");
            _logger.LogError($"{nameof(LoginController)} - {nameof(GetUserList)} - Error Level Log");
            _logger.LogCritical($"{nameof(LoginController)} - {nameof(GetUserList)} - Critical Level Log");

            return Ok(User);
        }
    }
}
