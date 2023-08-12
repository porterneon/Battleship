using Battleship.Enums;
using Battleship.Extensions;
using Battleship.Interfaces;
using Battleship.Models.Boards;

namespace Battleship.Services;

public class FiringBoard : IFiringBoard
{
    private readonly List<Sector> _sectors;

    public FiringBoard(List<Sector> sectors)
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
    
    public List<Coordinates> GetOpenRandomPanels()
    {
        return _sectors.Where(x => x.UsageType == UsageType.Empty && x.IsRandomAvailable()).Select(x=>x.GetCoordinates()).ToList();
    }

    public List<Coordinates> GetHitNeighbors()
    {
        var sectors = new List<Sector>();
        var hits = _sectors.Where(x => x.UsageType == UsageType.Hit);
        
        foreach(var hit in hits)
        {
            sectors.AddRange(GetNeighbors(hit.GetCoordinates()).ToList());
        }
        
        return sectors.Distinct().Where(x => x.UsageType == UsageType.Empty).Select(x => x.GetCoordinates()).ToList();
    }

    private IEnumerable<Sector> GetNeighbors(Coordinates coordinates)
    {
        var row = coordinates.Row;
        var column = coordinates.Column;
        
        var sectors = new List<Sector>();

        Sector? sector = null;
        if (column > 1)
        {
            sector = GetSector(row, column - 1);
        }
        
        if (row > 1)
        {
            sector = GetSector(row - 1, column);
        }
        
        if (row < 10)
        {
            sector = GetSector(row + 1, column);
        }
        
        if (column < 10)
        {
            sector = GetSector(row, column + 1);
        }

        if (sector is not null)
            sectors.Add(sector);
        
        return sectors;
    }
}