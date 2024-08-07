using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithNet8.Api.Business;
using RestWithNet8.Api.Data.VO;
using RestWithNet8.Api.Hipermedia.Filters;

namespace RestWithNet8.Api.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private readonly ILoginBusiness _loginBusiness;

        public AuthController(ILogger<BookController> logger, ILoginBusiness loginBusiness)
        {
            _logger = logger;
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null)
                return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(user);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null)
                return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(tokenVo);
            if (token == null)
                return BadRequest("Invalid client request");

            return Ok(token);
        }
        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(userName);
            if (!result)
                return BadRequest("Invalid client request");

            return NoContent();
        }
    }
}
