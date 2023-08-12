using Battleship.Enums;

namespace Battleship.Interfaces;

public interface IGameConfiguration
{
    int GetBoardSize();
    Dictionary<ShipType, int> GetShipConfiguration();
}