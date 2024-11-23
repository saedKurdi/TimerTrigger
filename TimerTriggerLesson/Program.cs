using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TimerTriggerLesson.Services.Abstract;
using TimerTriggerLesson.Services.Concrete;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddSingleton<IQueueService>(qs => 
        new QueueService ("DefaultEndpointsProtocol=https;AccountName=steptimertrigger;AccountKey=UJtUDIJjTDdLGUpcpyaQTsyb2EgoTR+ebfcHIy9a8cyutz9Jg57fNhqVhk/pwDMYCZSTyoAV7YAo+ASt1V7P5A==;BlobEndpoint=https://steptimertrigger.blob.core.windows.net/;QueueEndpoint=https://steptimertrigger.queue.core.windows.net/;TableEndpoint=https://steptimertrigger.table.core.windows.net/;FileEndpoint=https://steptimertrigger.file.core.windows.net/;", "product")
    );

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
