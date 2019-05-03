using Newtonsoft.Json;

namespace AspNetCoreDemo.Models
{
    public class Message
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
