using Microsoft.AspNetCore.Mvc;

using WebDemo.Services;

namespace WebDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaService _captchaService;

        public CaptchaController(ICaptchaService captchaService)
        {
            _captchaService = captchaService;
        }

        /// Get the graphical captcha
        /// <param name="dysopsia">Whether visual impairment mode is enabled</param>
        /// <returns>Graphical captcha</returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool dysopsia = false)
        {
            CaptchaPayload captcha = await _captchaService
                .GenerateCaptchaAsync(dysopsia);

            HttpContext.Session.SetString("cid", captcha.CaptchaId);
            HttpContext.Response.Headers
                .Add("X-Content-Type-Options", "nosniff");

            return File(captcha.ImageBytes, "image/png");
        }

        /// Check if the user-clicked positions on the graphical captcha are correct
        /// <param name="answer">List of user-clicked coordinates</param>
        /// <returns>Result</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<string>> Check([FromQuery] string answer)
        {
            string? captchaId = HttpContext.Session.GetString("cid");
            if (captchaId == null)
                return BadRequest();

            bool result = await _captchaService
            .VerifyCaptchaAsync(captchaId, answer);
                return result ? "Success, fully matched" : "~~~~Wrong~~~~";
        }
    }
}
