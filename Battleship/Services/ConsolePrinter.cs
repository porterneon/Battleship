using Battleship.Interfaces;

namespace Battleship.Services;

public class ConsolePrinter : IOutputPrinter
{
    public void PrintEndMessage(IPlayer player1, IPlayer player2)
    {
        Console.WriteLine(player1.HasLost() ? ">>>>> You have lost the game. <<<<<" : ">>>>> You WIN! <<<<<");
        Console.WriteLine();
        Console.WriteLine("Please pres Enter to preview game boards.");
        Console.ReadLine();

        Console.WriteLine("===================================");
        Console.WriteLine("Player Board:");
        player1.PrintBoards();
        
        Console.WriteLine();
        Console.WriteLine();
        
        Console.WriteLine("===================================");
        Console.WriteLine("AI Board:");
        player2.PrintBoards();
    }

    public void PrintMessage(string message)
    {
        Console.WriteLine(message);
    }
    
}