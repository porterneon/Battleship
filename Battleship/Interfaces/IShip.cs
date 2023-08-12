using Battleship.Enums;
using Battleship.Models.Boards;
using Battleship.Services;

namespace Battleship.Interfaces;

public interface IShip
{
    string Name { get; set; }
    int Size { get; set; }
    int Hits { get; set; }
    UsageType UsageType { get; set; }

    List<Sector> Sectors { get; set; }
    
    bool IsSunk()
    {
        return Hits >= Size;
    }
}