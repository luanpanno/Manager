using Manager.Api.Token;
using Manager.Api.Utilities;
using Manager.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace Manager.Api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [Route("sign-in")]
        public IActionResult LoginAsync(
            [FromBody] LoginViewModel model
        )
        {
            try
            {
                var tokenLogin = _configuration["Jwt:Login"];
                var tokenPassword = _configuration["Jwt:Password"];

                if (model.Login == tokenLogin && model.Password == tokenPassword)
                {
                    var response = new ResultViewModel
                    {
                        Message = "Authenticated successfully",
                        Success = true,
                        Data = new
                        {
                            Token = _tokenGenerator.GenerateToken(),
                            TokenExpires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:Expire"]))
                        }
                    };

                    return Ok(response);
                }

                return StatusCode(401, Responses.UnauthorizedErrorMessage());
            }
            catch (Exception exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage(exception.Message));
            }
        }
    }
}
