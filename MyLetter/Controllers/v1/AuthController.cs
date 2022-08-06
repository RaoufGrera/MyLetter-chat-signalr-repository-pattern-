using Microsoft.AspNetCore.Mvc;
using MyLetter.Services;

namespace MyLetter.Controllers.v1
{
    [Route("v1/[controller]")]
    public class AuthController : ControllerBase
    {

        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpGet]
        [Route("login/facebook")]
        public IActionResult LoginByFacebook([FromBody] string accessToken)
        {
            var user = _authService.LoginByFacebook(accessToken);
            if (user.Result == null)
                return BadRequest(new { message = "خطاء اثناء تسجيل الدخول" });
            return Ok(user.Result);
        }

        [HttpGet]
        [Route("login/gmail")]
        public IActionResult LoginByGmail([FromBody] string accessToken)
        {
            var user = _authService.LoginByGmail(accessToken);

            if (user.Result == null)
                return BadRequest(new { message = "خطاء اثناء تسجيل الدخول" });
            return Ok(user.Result);
        }

    }
}
