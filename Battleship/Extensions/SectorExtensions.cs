using Battleship.Models.Boards;
using Battleship.Services;

namespace Battleship.Extensions;

public static class SectorExtensions
{
    public static List<Sector> Range(this List<Sector> sectors, int startRow, int startColumn, int endRow, int endColumn)
    {
        return sectors.Where(x => x.GetCoordinates().Row >= startRow 
                                 && x.GetCoordinates().Column >= startColumn 
                                 && x.GetCoordinates().Row <= endRow 
                                 && x.GetCoordinates().Column <= endColumn).ToList();
    }
}