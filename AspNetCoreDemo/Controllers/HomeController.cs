using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemo.Controllers
{
    
    [ApiController]
    public class HomeController : ControllerBase
    {
        [AllowAnonymous]
        [Route("home/error")]
        public object Error()
        {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var errorMessage = exceptionHandlerPathFeature?.Error?.Message;
            var path = exceptionHandlerPathFeature?.Path;

            return new { message = 
                $"Apologies, there has been a server error and we were unable to handle your request at this time.  " +
                $"Trying again may result in success. Details: {errorMessage}. Path: {path}" };
        }
    }
}