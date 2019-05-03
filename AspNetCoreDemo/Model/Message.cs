using Newtonsoft.Json;

namespace AspNetCoreDemo.Model
{
    public class Message
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
