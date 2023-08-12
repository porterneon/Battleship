using Battleship.Extensions;
using Battleship.Interfaces;

namespace Battleship.Services;

public class PlayerBoard : IGameBoard
{
    private readonly List<Sector> _sectors;
    
    public PlayerBoard(List<Sector> sectors)
    {
        _sectors = sectors;
    }

    public List<Sector> GetSectors(int startRow, int startColumn, int endRow, int endColumn)
    {
        return _sectors.Range(startRow, startColumn, endRow, endColumn);
    }
    
    public Sector? GetSector(int row, int column)
    {
        return _sectors.FirstOrDefault(x => x.GetCoordinates().Row == row && x.GetCoordinates().Column == column);
    }
}