using Battleship.Enums;
using Battleship.Interfaces;
using Battleship.Models;
using Battleship.Models.Boards;

namespace Battleship.Services;

public class BoardCreator : IBoardCreator
{
    public IGameBoard? CreateBoard(BoardType boardType, int boardSize)
    {
        return boardType switch
        {
            BoardType.PlayerBoard => CreatePlayerBoard(boardSize),
            BoardType.FiringBoard => CreateFiringBoard(boardSize),
            _ => throw new ArgumentException("Unsupported board type!", nameof(boardType))
        };
    }

    private static FiringBoard? CreateFiringBoard(int boardSize)
    {
        return new FiringBoard(CreateBoardSectors(boardSize));
    }

    private static PlayerBoard? CreatePlayerBoard(int boardSize)
    {
        return new PlayerBoard(CreateBoardSectors(boardSize));
    }
    
    private static List<Sector> CreateBoardSectors(int boardSize)
    {
        var sectors = new List<Sector>();
        for (var i = 0; i < boardSize; i++)
        {
            for (var j = 0; j < boardSize; j++)
            {
                sectors.Add(new Sector(new Coordinates(i, j)));
            }
        }

        return sectors;
    }
}