using Battleship.Enums;
using Battleship.Interfaces;
using Battleship.Models;
using Battleship.Models.Boards;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Battleship;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IGame _game;

    public Worker(ILogger<Worker> logger, IGame game)
    {
        _logger = logger;
        _game = game;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested || _game.GameEnded())
        {
            try
            {
                Console.WriteLine("Please provide coordinates for your shot");
                Console.WriteLine("Coordinates has to be in range A-J and 1-10. (Example: A5 or B10)");
                var userInput = GetUserInput();
                if (userInput != null)
                    _game.PlayRound(userInput);
            }
            catch (Exception e)
            {
                _logger.LogError("Application failed with error: {Message}", e.Message);
            }
        }
        
        return Task.CompletedTask;
    }

    private Coordinates? GetUserInput()
    {
        var userInput = Console.ReadLine();
        if (userInput is null || userInput.Length == 0 || userInput.Length > 3)
        {
            _logger.LogInformation("Incorrect Input");
            return null;
        }

        var columnString = userInput[0].ToString().ToUpper();
        if(!Enum.GetNames(typeof(RowNumber)).Contains(columnString))
        {
            _logger.LogInformation($"Incorrect row name: {columnString}");
            return null;
        }

        Enum.TryParse(columnString, true, out RowNumber column);
        
        
        if (!int.TryParse(userInput[1..], out var row))
        {
            _logger.LogInformation($"Incorrect column number: {userInput[1..]}");
            return null;
        }

        return new Coordinates(row - 1, (int)column);
    }
}