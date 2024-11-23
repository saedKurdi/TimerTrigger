using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TimerTriggerLesson.Services.Abstract;

namespace TimerTriggerLesson;

public class Function1
{
    private readonly ILogger _logger;
    private readonly IQueueService queueService;

    public Function1(ILoggerFactory loggerFactory, IQueueService queueService)
    {
        _logger = loggerFactory.CreateLogger<Function1>();
        this.queueService = queueService;
    }

    [Function("TimerTriggerFunction1")]
    public async Task Run([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer)
    {
        await WriteProductToQueueStorage();
    }

    [Function("TimerTriggerFunction2")]
    public async Task Run2([TimerTrigger("*/15 * * * * *")] TimerInfo myTimer)
    {
        await ReadProductFromQueueStorage();
    }

    // writing product to queue storage : 
    private async Task WriteProductToQueueStorage()
    {
        var productName = "Product" + Guid.NewGuid().ToString();
        await queueService.SendMessageAsync(productName);
        _logger.LogInformation($"{productName} was succesfully sent to azure queue storage .");
    }

    // reading product from queue storage : 
    private async Task ReadProductFromQueueStorage()
    {
        var productName = await queueService.ReceiveMessageAsync();
        if (productName == null)
        {
            _logger.LogError("No product has been read from queue storage .");
        }
        else
        {
            _logger.LogInformation($"{productName.Body.ToString()} was succesfully read and deleted from azure queue storage .");
        }

    }
}
