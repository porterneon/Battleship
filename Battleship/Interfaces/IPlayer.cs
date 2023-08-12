using Battleship.Enums;
using Battleship.Models.Boards;

namespace Battleship.Interfaces;

public interface IPlayer
{
    string Name { get; set; }
    bool HasLost();
    ShotResult ProcessShot(Coordinates coords);
    void ProcessShotResult(Coordinates coords, ShotStatus status);
    void PrintBoards();
}