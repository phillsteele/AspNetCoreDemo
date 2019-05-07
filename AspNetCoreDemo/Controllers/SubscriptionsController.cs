using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemo.Model.SlcsInbound.Fulfil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemo.Controllers
{
    // ApiController:  https://devblogs.microsoft.com/aspnet/asp-net-core-2-1-web-apis/
    // Using the [ApiController] attribute gives us some default validation handling of binding errors
    // For example an invalid guid supplied as part of the subscription message gives this response:

    //  {
    //    "errors": {
    //        "subscriptionGroupId": [
    //            "Error converting value \"123\" to type 'System.Nullable`1[System.Guid]'. Path 'subscriptionGroupId', line 2, position 30."
    //        ]
    //    },
    //    "title": "One or more validation errors occurred.",
    //    "status": 400,
    //    "traceId": "0HLMFM4HSPR3S:00000001"
    //  }

    // If model binding fails for any reason subscription will be null, for example attempting to assign a non-guid to a guid

    [Route("api/[controller]")]
    public class SubscriptionsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Fulfil([FromBody] Subscription subscription)
        {

            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Fulfil(Guid? subscriptionId)
        {

            return Ok(new { name = "123" });
        }
    }
}