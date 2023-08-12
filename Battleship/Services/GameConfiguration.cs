using Battleship.Enums;
using Battleship.Interfaces;

namespace Battleship.Services;

/// <summary>
/// In finale version game configuration could be read from app settings. 
/// </summary>
public class GameConfiguration : IGameConfiguration
{
    public int GetBoardSize()
    {
        return 10;
    }
    
    public Dictionary<ShipType, int> GetShipConfiguration()
    {
        return new Dictionary<ShipType, int>()
        {
            { ShipType.Battleship , 1 },
            { ShipType.Destroyer , 2 }
        };
    }
}