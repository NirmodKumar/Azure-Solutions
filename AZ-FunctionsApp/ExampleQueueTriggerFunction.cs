using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AZ_FunctionsApp
{
    public class ExampleQueueTriggerFunction
    {
        private readonly ILogger<ExampleQueueTriggerFunction> _logger;

        public ExampleQueueTriggerFunction(ILogger<ExampleQueueTriggerFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ExampleQueueTriggerFunction))]
        public void Run([QueueTrigger("myqueue-items", Connection = "StorageAccountConnectionString")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
