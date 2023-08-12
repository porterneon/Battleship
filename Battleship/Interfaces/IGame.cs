using Battleship.Models.Boards;

namespace Battleship.Interfaces;

public interface IGame
{
    void PlayRound(Coordinates coordinates);
    bool GameEnded();
}