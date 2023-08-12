using Battleship.Enums;

namespace Battleship.Interfaces;

public interface IShipManager
{
    List<IShip> GenerateShipCollection(IDictionary<ShipType, int> shipConfiguration);
    void PlaceShipsRandomly(List<IShip> ships, IGameBoard gameBoard, int boardSize);
}