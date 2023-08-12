using Battleship.Interfaces;

namespace Battleship.Services;

public class UserPlayer : Player, IPlayer
{
    public string Name { get; set; } = "User";

    public UserPlayer(IShipManager shipManager, IGameConfiguration gameConfiguration, IBoardCreator boardCreator, IOutputPrinter outputPrinter) 
        : base(shipManager, gameConfiguration, boardCreator, outputPrinter)
    {
    }
}