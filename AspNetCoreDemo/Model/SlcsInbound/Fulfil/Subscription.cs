using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreDemo.Model.SlcsInbound.Fulfil
{
    public class Subscription : Message
    {
        /// <summary>
        /// If this is not specified then we need to create it.
        /// Optional field.
        /// </summary>
        //[Guid]
        public Guid? subscriptionGroupId { get; set; }

        /// <summary>
        /// The client reference is meaningful only to the client.
        /// Optional field.
        /// </summary>
        [MaxLength(100)]
        public string clientRef { get; set; }
    }
}
