using Battleship.Enums;

namespace Battleship.Interfaces;

public interface IBoardCreator
{
    IGameBoard? CreateBoard(BoardType boardType, int boardSize);
}