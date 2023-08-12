using Battleship.Enums;
using Battleship.Models.Boards;

namespace Battleship.Interfaces;

public interface ISector
{
    UsageType UsageType { get; set; }
    Guid Id { get; init; }
    string? GetStatus();
    bool IsOccupied();
    Coordinates GetCoordinates();
    bool IsRandomAvailable();
}