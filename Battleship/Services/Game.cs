using Battleship.Interfaces;
using Battleship.Models.Boards;

namespace Battleship.Services;

public class Game : IGame
{
    private readonly IPlayer _player1;
    private readonly IAiPlayer _player2;
    private readonly IOutputPrinter _consolePrinter;

    public Game(IPlayer player1, IAiPlayer player2, IOutputPrinter outputPrinter)
    {
        _player1 = player1;
        _player2 = player2;
        _consolePrinter = outputPrinter;
        
        StartGame();
    }
    
    public bool GameEnded()
    {
        return _player1.HasLost() || _player2.HasLost();
    }
    
    public void PlayRound(Coordinates coordinates)
    {
        var result = _player2.ProcessShot(coordinates);
        
        if (result.Ship != null && result.Ship.IsSunk())
        {
            _consolePrinter.PrintMessage(_player2.Name + " says: \"You sunk my " + result.Ship.Name + "!\"");
        }
        
        _player1.ProcessShotResult(coordinates, result.Status);

        if (!_player2.HasLost())
        {
            coordinates = _player2.FireShot();
            result = _player1.ProcessShot(coordinates);
            _player2.ProcessShotResult(coordinates, result.Status);
        }
        
        if(!GameEnded())
            _player1.PrintBoards();
        else
        {
            _consolePrinter.PrintEndMessage(_player1, _player2);
        }
    }
    
    private void StartGame()
    {
        _player1.PrintBoards();

        // Uncomment to cheat and see opponent's board :) 
        //_player2.PrintBoards();
    }
}