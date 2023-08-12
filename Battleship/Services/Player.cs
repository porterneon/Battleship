using System.Text;
using Battleship.Enums;
using Battleship.Interfaces;
using Battleship.Models.Boards;

namespace Battleship.Services;

public abstract class Player
{
    private readonly List<IShip>? _ships;
    private readonly IGameBoard? _playerBoard;
    private readonly IOutputPrinter _outputPrinter;
    protected readonly IFiringBoard? FiringBoard;

    protected Player(IShipManager shipManager, IGameConfiguration gameConfiguration, IBoardCreator boardCreator, IOutputPrinter outputPrinter)
    {
        _outputPrinter = outputPrinter;
        var boardSize = gameConfiguration.GetBoardSize();
        _playerBoard = boardCreator.CreateBoard(BoardType.PlayerBoard, boardSize);
        FiringBoard = (IFiringBoard)boardCreator.CreateBoard(BoardType.FiringBoard, boardSize)!;
        _ships = shipManager.GenerateShipCollection(gameConfiguration.GetShipConfiguration());
        
        if (_ships is null)
            throw new ApplicationException("Collection o ships has not been initialized properly");
        
        if (_playerBoard is null)
            throw new ApplicationException("PlayerBoard has not been initialized properly");
        
        if (FiringBoard is null)
            throw new ApplicationException("FiringBoard has not been initialized properly");
        
        shipManager.PlaceShipsRandomly(_ships, _playerBoard!, boardSize);
    }
    
    public bool HasLost()
    {
        return _ships is not null && _ships.All(s => s.IsSunk());
    }
    
    public ShotResult ProcessShot(Coordinates coords)
    {
        var sector = _playerBoard!.GetSector(coords.Row, coords.Column);
        if (sector is null)
            throw new ArgumentOutOfRangeException(nameof(coords));
        
        if(!sector.IsOccupied())
        {
            return new ShotResult(null, ShotStatus.Miss);
        }
        
        var ship = _ships!.FirstOrDefault(x => x.Sectors.Select(s => s.Id).Contains(sector.Id));
        
        if(ship is null)
            return new ShotResult(null, ShotStatus.Miss);
        
        ship.Hits++;
        
        return new ShotResult(ship, ShotStatus.Hit);
    }

    public void ProcessShotResult(Coordinates coords, ShotStatus status)
    {
        var sector = FiringBoard!.GetSector(coords.Row, coords.Column);
        if(sector is null)
            return;
        
        switch(status)
        {
            case ShotStatus.Hit:
                sector.UsageType = UsageType.Hit;
                break;

            case ShotStatus.Miss:
            default:
                sector.UsageType = UsageType.Miss;
                break;
        }
    }
    
    public void PrintBoards()
    {
        var stringBuilder = new StringBuilder();
        try
        {
            stringBuilder.AppendLine("Own Board:                          Firing Board:");
            stringBuilder.AppendLine("   A B C D E F H G I J                    A B C D E F H G I J");

            for (var row = 0; row < 10; row++)
            {
                stringBuilder.Append(row == 9 ? (row + 1) + " " : (row + 1) + "  ");
                for (var ownColumn = 0; ownColumn < 10; ownColumn++)
                {
                    stringBuilder.Append(_playerBoard!.GetSector(row, ownColumn)?.GetStatus() + " ");
                }

                stringBuilder.Append("                ");
                stringBuilder.Append(row == 9 ? (row + 1) + " " : (row + 1) + "  ");
                for (var firingColumn = 0; firingColumn < 10; firingColumn++)
                {
                    stringBuilder.Append(FiringBoard!.GetSector(row, firingColumn)?.GetStatus() + " ");
                }

                stringBuilder.AppendLine(Environment.NewLine);
            }

            stringBuilder.AppendLine(Environment.NewLine);

            _outputPrinter.PrintMessage(stringBuilder.ToString());
        }
        finally
        {
            stringBuilder.Clear();
        }
    }
}