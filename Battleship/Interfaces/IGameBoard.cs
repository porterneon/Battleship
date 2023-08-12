using Battleship.Models.Boards;
using Battleship.Services;

namespace Battleship.Interfaces;

public interface IGameBoard
{
    List<Sector> GetSectors(int startRow, int startColumn, int endRow, int endColumn);
    Sector? GetSector(int row, int column);
}