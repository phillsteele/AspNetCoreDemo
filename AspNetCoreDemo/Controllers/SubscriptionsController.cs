using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDemo.Authorization;
using AspNetCoreDemo.Model.SlcsInbound.Fulfil;
using AspNetCoreDemo.Security;
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
    //
    //
    // We can check the ModelState.IsValid and return an automatic response based on the ModelState as shown.

    //if (!ModelState.IsValid)
    //{
    //    return this.BadRequest(ModelState);
    //}

    // This produces an error that looks like:
    // 400 status code
    //{
    //    "subscriptionGroupId": [
    //        "Error converting value \"e68e181c-0317-4547-a529-bfe2f22096d\" to type 'System.Nullable`1[System.Guid]'. Path 'subscriptionGroupId', line 2, position 62."
    //    ]
    //}

    
    public class SubscriptionsController : Controller
    {
        /// <summary>
        /// Create a subscription
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("api/[controller]")]
        //[AllowAnonymous] -- This attribute allows us to poke a hole in the Authorization mechanism.  Useful if we need a publicly available endpoint.
        public async Task<IActionResult> Post([FromBody] Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            return Ok();
        }


        //[Authorize("bob")]
        // If there is no policy called "bob" defined then calling this method will result in an exception: "The AuthorizationPolicy named: 'bob' was not found."
        // If the policy fails then the user is returned a 403 - Forbidden

        //[Authorize(Policies.HasClaim)]
        // Policies can be handled by a custom handler, i.e. HasClaimPolicyHandler

        /// <summary>
        /// Fetch an existing subscription
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        [HttpGet]
        [HasClaimAuthorize(CustomClaimTypes.FulfilGet)]
        [Route("api/[controller]/{subscriptionId}")]
        // This will insist that the caller has the claim "Fulfil.Get"
        public async Task<IActionResult> Get([FromRoute] Guid? subscriptionId)
        {
            return Ok(new { name = "123" });
        }

        /// <summary>
        /// Delete a subscription
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <returns></returns>
        [HttpDelete]
        [HasClaimAuthorize]
        [Route("api/[controller]/{subscriptionId}")]
        // As no claim has been specified the default behaviour will apply which means that the user must be authenticated
        public async Task<IActionResult> Delete([FromRoute] Guid? subscriptionId)
        {
            return Ok(new { deleted = true });
        }
    }
}