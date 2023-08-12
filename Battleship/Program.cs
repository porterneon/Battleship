using Battleship;
using Battleship.Interfaces;
using Battleship.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IGameConfiguration, GameConfiguration>();
        services.AddScoped<IShipManager, ShipManager>();
        services.AddScoped<IPlayer, UserPlayer>();
        services.AddScoped<IAiPlayer, AiPLayer>();
        services.AddScoped<IGame, Game>();
        services.AddScoped<IBoardCreator, BoardCreator>();
        services.AddScoped<IOutputPrinter, ConsolePrinter>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
