using Battleship.Enums;
using Battleship.Interfaces;

namespace Battleship.Services;

public class ShipManager : IShipManager
{
    public List<IShip> GenerateShipCollection(IDictionary<ShipType, int> shipConfiguration)
    {
        var ships = new List<IShip>();

        foreach (var (shipType, quantity) in shipConfiguration)
        {
            for (var i = 0; i < quantity; i++)
            {
                switch (shipType)
                {
                    case ShipType.Destroyer:
                        ships.Add(new Models.Ships.Destroyer());
                        break;
                    case ShipType.Battleship:
                        ships.Add(new Models.Ships.Battleship());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(shipConfiguration));
                }
            }
        }
        
        return ships;
    }

    public void PlaceShipsRandomly(List<IShip> ships, IGameBoard gameBoard, int boardSize)
    {
        var rand = new Random(Guid.NewGuid().GetHashCode());

        foreach (var ship in ships)
        {
            var isNotCorrect = true;
            while (isNotCorrect)
            {
                var startColumn = rand.Next(boardSize);
                var startRow = rand.Next(boardSize);
                var endColumn = startColumn;
                var endRow = startRow;
                var orientation = rand.Next(boardSize ^ 2) % 2; // 0 for Horizontal
                
                if (orientation == 0)
                {
                    endColumn += ship.Size - 1;
                }
                else
                {
                    endRow += ship.Size - 1;
                }
                
                if(endRow >= boardSize || endColumn >= boardSize)
                {
                    isNotCorrect = true;
                    continue;
                }

                var affectedSectors = gameBoard.GetSectors(startRow, startColumn, endRow, endColumn);
                if (affectedSectors.Any(x => x.IsOccupied()))
                {
                    isNotCorrect = true;
                    continue;
                }

                foreach (var sector in affectedSectors)
                {
                    sector.UsageType = ship.UsageType;
                    ship.Sectors.Add(sector);
                }

                isNotCorrect = false;
            }
        }
    }
}