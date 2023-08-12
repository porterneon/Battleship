using Battleship.Enums;
using Battleship.Interfaces;
using Battleship.Models.Boards;
using Battleship.Services;

namespace Battleship.Models.Ships;

public class Destroyer : IShip
{
    public string Name { get; set; } = "Destroyer";
    public int Size { get; set; } = 4;
    public int Hits { get; set; }
    public UsageType UsageType { get; set; } = UsageType.Destroyer;
    public List<Sector> Sectors { get; set; } = new List<Sector>();
}