using MichelMichels.DobissSharp;
using MichelMichels.DobissSharp.Api;
using MichelMichels.DobissSharp.Services;
using MichelMichels.DobissSharp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp.Authenticators;
using System.Diagnostics;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices(services =>
{
    services.AddLogging(logging =>
    {
        logging.SetMinimumLevel(LogLevel.Trace);
    });
    services.AddSingleton<IConfigurationService>(new ConfigurationService("dobiss_settings.json"));    
    services.AddSingleton<IJwtTokenGenerator, DobissJwtTokenGenerator>();
    services.AddSingleton<IAuthenticator, DobissAuthenticator>();
    services.AddSingleton<IDobissLightController, DobissLightController>();
    services.AddSingleton<IDobissRestClient, DobissRestClient>();
    services.AddSingleton<IDobissService, DobissService>();
});

var host = builder.Build();
var task = host.RunAsync();

var dobissService = host.Services.GetRequiredService<IDobissService>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

var outputs = await dobissService.GetOutputs();
var lights = outputs.OfType<Light>();

while (true)
{
    logger.LogInformation("Menu");
    logger.LogInformation("----");
    logger.LogInformation("1. Turn off all");
    logger.LogInformation("2. Turn on all");
    logger.LogInformation("3. Toggle all");
    logger.LogInformation("4. Print lights");

    int menuItemId = 0;
    while (menuItemId == 0 || menuItemId > 4)
    {
        var input = Console.ReadLine();
        int.TryParse(input, out menuItemId);
    }

    foreach (var light in lights)
    {
        switch (menuItemId)
        {
            case 1: light.TurnOff(); break;
            case 2: light.TurnOn(); break;
            case 3: light.Toggle(); break;
            case 4: logger.LogInformation(light.Name); break;
        }

        Thread.Sleep(100);
        //await dobissService.GetStatus(light);
    }
}

//var status = await dobissService.GetStatusAll();
Console.ReadLine();