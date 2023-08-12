namespace Battleship.Interfaces;

public interface IOutputPrinter
{
    void PrintEndMessage(IPlayer player1, IPlayer player2);
    void PrintMessage(string message);
}