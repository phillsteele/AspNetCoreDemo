using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AspNetCoreDemo.Models.SlcsOutbound
{
    public class SlcsErrors : Message
    {
        public SlcsErrors() { }

        public SlcsErrors(SlcsErrorCollection errorList)
        {
            errors = errorList;
        }

        public SlcsErrorCollection errors { get; set; } = new SlcsErrorCollection();

        public static SlcsErrors WrapError(SlcsError error)
        {
            var dummy = error ?? throw new ArgumentNullException(nameof(error));
            return new SlcsErrors { errors = new SlcsErrorCollection { error } };
        }
    }

    public class SlcsErrorCollection : List<SlcsError> { };

    public class SlcsError
    {
        public string code { get; set; }
        public string source { get; set; }
        public string data { get; set; }
        public string description { get; set; }

        [JsonIgnore]
        public bool retrySuggested { get; set; }
    }
}
