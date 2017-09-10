using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Email.WelcomeEmail
{
    public static class Welcome
    {
        [FunctionName("Welcome")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "email/welcome")] HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var content = req.Content;
            string jsonContent = content.ReadAsStringAsync().Result;

            WelcomeModel welcome = JsonConvert.DeserializeObject<WelcomeModel>(jsonContent);

            // Fetching the name from the path parameter in the request URL
            return req.CreateResponse(HttpStatusCode.OK, $"Hello {welcome.Name} at {welcome.Email}");
        }
    }
}
