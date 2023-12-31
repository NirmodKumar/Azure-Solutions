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
        public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "BlobStorageConnectionString")] byte[] fileBytes, string name)
        {
            var stream = new MemoryStream(fileBytes);
            using var blobStreamReader = new StreamReader(stream);
            var content = await blobStreamReader.ReadToEndAsync();
            _logger.LogInformation($"C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}");
        }
    }
}
