using MichelMichels.DobissSharp;
using MichelMichels.DobissSharp.Api;
using MichelMichels.DobissSharp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp.Authenticators;

var builder = Host.CreateDefaultBuilder();

builder.ConfigureServices(services =>
{
    services.AddSingleton<IConfigurationService>(new ConfigurationService("dobiss_settings.json"));
    services.AddSingleton<IJwtTokenGenerator, DobissJwtTokenGenerator>();
    services.AddSingleton<IAuthenticator, DobissAuthenticator>();
    services.AddSingleton<IDobissRestClient, DobissRestClient>();
    services.AddSingleton<IDobissService, DobissService>();
});

var host = builder.Build();
var task = host.RunAsync();

var dobissService = host.Services.GetRequiredService<IDobissService>();

var rooms = await dobissService.GetRooms();
var elements = await dobissService.GetNXTElements();

foreach(var element in elements)
{
    Console.WriteLine($"{element.ModuleId,-3} | {element.ChannelId,-3} | {element.DeviceType, -15 } | {element.Name,-20}");
}

//foreach(var room in rooms)
//{
//    Console.ForegroundColor = ConsoleColor.Green;
//    Console.WriteLine($"{room.Name} ({room.Elements.Count})");

//    Console.ForegroundColor = ConsoleColor.White;
//    foreach(var element in room.Elements)
//    {
//        Console.WriteLine($"[{element.ModuleId}:{element.ChannelId}] Name: {element.Name}; Device type: {element.DeviceType}");
//    }

//    Console.WriteLine();
//}

Console.ReadLine();