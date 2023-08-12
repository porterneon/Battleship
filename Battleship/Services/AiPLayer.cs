using Battleship.Interfaces;
using Battleship.Models.Boards;

namespace Battleship.Services;

public class AiPLayer : Player, IAiPlayer
{
    public string Name { get; set; } = "AI";

    public AiPLayer(IShipManager shipManager, IGameConfiguration gameConfiguration, IBoardCreator boardCreator, IOutputPrinter outputPrinter) 
        : base(shipManager, gameConfiguration, boardCreator, outputPrinter)
    {
    }
    
    public Coordinates FireShot()
    {
        List<Coordinates> hitNeighbors = FiringBoard!.GetHitNeighbors();
        Coordinates coords = hitNeighbors.Any() ? SearchingShot() : RandomShot();
        return coords;
    }
    
    private Coordinates RandomShot()
    {
        List<Coordinates> availablePanels = FiringBoard!.GetOpenRandomPanels();
        var rand = new Random(Guid.NewGuid().GetHashCode());
        int panelId = rand.Next(availablePanels.Count);
        return availablePanels[panelId];
    }

    private Coordinates SearchingShot()
    {
        var rand = new Random(Guid.NewGuid().GetHashCode());
        List<Coordinates> hitNeighbors = FiringBoard!.GetHitNeighbors();
        int neighborId = rand.Next(hitNeighbors.Count);
        return hitNeighbors[neighborId];
    }
}