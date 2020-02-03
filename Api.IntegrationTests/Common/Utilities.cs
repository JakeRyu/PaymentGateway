using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Api.IntegrationTests.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}