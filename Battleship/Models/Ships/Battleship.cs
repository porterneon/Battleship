using Battleship.Enums;
using Battleship.Interfaces;
using Battleship.Models.Boards;
using Battleship.Services;

namespace Battleship.Models.Ships;

public class Battleship : IShip
{
    public string Name { get; set; } = "Battleship";
    public int Size { get; set; } = 5;
    public int Hits { get; set; }
    public UsageType UsageType { get; set; } = UsageType.Battleship;
    public List<Sector> Sectors { get; set; } = new List<Sector>();
}