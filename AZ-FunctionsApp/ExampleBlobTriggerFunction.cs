using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AZ_FunctionsApp
{
    public class ExampleBlobTriggerFunction
    {
        private readonly ILogger<ExampleBlobTriggerFunction> _logger;

        public ExampleBlobTriggerFunction(ILogger<ExampleBlobTriggerFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ExampleBlobTriggerFunction))]
        public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "BlobStorageConnectionString")] string myTriggerItem, string name)
        {
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {myTriggerItem}");
        }
    }
}
