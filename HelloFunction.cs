using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Configuration;

namespace FirstFunc
{
    public static class HelloFunction
    {
        [FunctionName("HelloFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string secret = System.Environment.GetEnvironmentVariable("first_func_secret");

            string responseMessage = string.IsNullOrEmpty(name)
                ? $"This HTTP triggered function executed successfully. Secret: {secret}"
                : $"Hello, {name}. Secret: {secret}";

            return new OkObjectResult(responseMessage);
        }
    }
}
