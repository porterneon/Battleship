using Battleship.Models.Boards;

namespace Battleship.Interfaces;

public interface IAiPlayer : IPlayer
{
    Coordinates FireShot();
}