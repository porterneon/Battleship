using Battleship.Models.Boards;

namespace Battleship.Interfaces;

public interface IFiringBoard : IGameBoard
{
    List<Coordinates> GetOpenRandomPanels();
    List<Coordinates> GetHitNeighbors();
}