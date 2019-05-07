using AspNetCoreDemo.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreDemo.Model.SlcsInbound.Fulfil
{
    public class Subscription : Message
    {
        // Model binding will fail if this is not a guid - we see a null object passed to the controller in this case
        public Guid? subscriptionGroupId { get; set; }

        // Model binding should occur but model validation will fail if this is not a guid
        [Guid]
        public string subscriptionId { get; set; }

        // Model binding should occur but model validation will fail if this is the wrong length
        [MaxLength(10)]
        public string clientRef { get; set; }
    }
}
