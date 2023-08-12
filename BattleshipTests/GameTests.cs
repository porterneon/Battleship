using Battleship.Enums;
using Battleship.Interfaces;
using Battleship.Models.Boards;
using Battleship.Services;

namespace BattleshipTests;

public class GameTests
{
    [Fact]
    public void GameEnded_ShouldReturnFalse()
    {
        // Arrange
        var player1 = Substitute.For<IPlayer>();
        player1.HasLost().Returns(false);
        
        var aiPlayer = Substitute.For<IAiPlayer>();
        aiPlayer.HasLost().Returns(false);
        
        var outputPrinter = Substitute.For<IOutputPrinter>();
        
        var service = new Game(player1, aiPlayer, outputPrinter);

        // Act
        var actual = service.GameEnded();

        // Assert
        Assert.False(actual);
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public void GameEnded_ShouldReturnTrue(bool playerHasLost, bool aiHasLost)
    {
        // Arrange
        var player1 = Substitute.For<IPlayer>();
        player1.HasLost().Returns(playerHasLost);
        
        var aiPlayer = Substitute.For<IAiPlayer>();
        aiPlayer.HasLost().Returns(aiHasLost);
        
        var outputPrinter = Substitute.For<IOutputPrinter>();
        
        var service = new Game(player1, aiPlayer, outputPrinter);

        // Act
        var actual = service.GameEnded();

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void PlayRound_ShouldProcessSuccessfullyNotEndingGame()
    {
        // Arrange
        var ship = Substitute.For<IShip>();
        ship.IsSunk().Returns(true);
        
        var player1Coordinates = new Coordinates(2, 5);
        var player1ShotStatus = ShotStatus.Hit;
        var aiPlayerCoordinates = new Coordinates(6, 3);
        var aiPlayerShotStatus = ShotStatus.Miss;
        
        var player1ShotResult = new ShotResult(ship, player1ShotStatus);
        var aiPlayerShotResult = new ShotResult(null, aiPlayerShotStatus);
        
        var player1 = Substitute.For<IPlayer>();
        player1.HasLost().Returns(false);
        player1.ProcessShotResult(player1Coordinates, player1ShotStatus);
        player1.ProcessShot(aiPlayerCoordinates).Returns(aiPlayerShotResult);
        player1.PrintBoards();
        
        var aiPlayer = Substitute.For<IAiPlayer>();
        aiPlayer.HasLost().Returns(false);
        aiPlayer.ProcessShot(player1Coordinates).Returns(player1ShotResult);
        aiPlayer.FireShot().Returns(aiPlayerCoordinates);
        aiPlayer.ProcessShotResult(aiPlayerCoordinates, aiPlayerShotStatus);
        
        var outputPrinter = Substitute.For<IOutputPrinter>();
        outputPrinter.PrintMessage(Arg.Any<string>());
        
        var service = new Game(player1, aiPlayer, outputPrinter);
        
        // Act
        service.PlayRound(player1Coordinates);
        
        // Assert
        aiPlayer.Received(1).ProcessShot(player1Coordinates);
        outputPrinter.Received(1).PrintMessage(Arg.Any<string>());
        player1.Received().ProcessShotResult(player1Coordinates, player1ShotStatus);
        aiPlayer.Received(1).FireShot();
        player1.Received(1).ProcessShot(aiPlayerCoordinates);
        aiPlayer.Received().ProcessShotResult(aiPlayerCoordinates, aiPlayerShotStatus);
        player1.Received().PrintBoards();
        outputPrinter.Received(0).PrintEndMessage(player1, aiPlayer);
    }

    [Fact]
    public void PlayRound_WhenAiLost_ShouldPrintEndMessage()
    {
        // Arrange
        var ship = Substitute.For<IShip>();
        ship.IsSunk().Returns(true);
        
        var player1Coordinates = new Coordinates(2, 5);
        var player1ShotStatus = ShotStatus.Hit;
        var aiPlayerCoordinates = new Coordinates(6, 3);
        var aiPlayerShotStatus = ShotStatus.Miss;
        
        var player1ShotResult = new ShotResult(ship, player1ShotStatus);
        var aiPlayerShotResult = new ShotResult(null, aiPlayerShotStatus);
        
        var player1 = Substitute.For<IPlayer>();
        player1.HasLost().Returns(false);
        player1.ProcessShotResult(player1Coordinates, player1ShotStatus);
        player1.ProcessShot(aiPlayerCoordinates).Returns(aiPlayerShotResult);
        player1.PrintBoards();
        
        var aiPlayer = Substitute.For<IAiPlayer>();
        aiPlayer.HasLost().Returns(true);
        aiPlayer.ProcessShot(player1Coordinates).Returns(player1ShotResult);
        aiPlayer.FireShot().Returns(aiPlayerCoordinates);
        aiPlayer.ProcessShotResult(aiPlayerCoordinates, aiPlayerShotStatus);
        
        var outputPrinter = Substitute.For<IOutputPrinter>();
        outputPrinter.PrintMessage(Arg.Any<string>());
        
        var service = new Game(player1, aiPlayer, outputPrinter);
        
        // Act
        service.PlayRound(player1Coordinates);
        
        // Assert
        aiPlayer.Received(1).ProcessShot(player1Coordinates);
        outputPrinter.Received(1).PrintMessage(Arg.Any<string>());
        player1.Received().ProcessShotResult(player1Coordinates, player1ShotStatus);
        aiPlayer.Received(0).FireShot();
        player1.Received(0).ProcessShot(aiPlayerCoordinates);
        aiPlayer.Received().ProcessShotResult(aiPlayerCoordinates, aiPlayerShotStatus);
        outputPrinter.Received(1).PrintEndMessage(player1, aiPlayer);
    }
}