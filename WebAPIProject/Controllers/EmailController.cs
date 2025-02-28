using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;

namespace WebAPIProject.Controllers
{

    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        //[HttpPost("send")]
        //public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        //{
        //    var result = await _emailService.SendEmailAsync(request.Id);
        //    return Ok(result);
        //}


        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { isSuccess = false, message = "Request cannot be null!" });
            }

            var result = await _emailService.SendEmail(request.Id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }



    }

}

