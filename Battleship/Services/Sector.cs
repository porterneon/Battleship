using System.ComponentModel;
using Battleship.Enums;
using Battleship.Extensions;
using Battleship.Interfaces;
using Battleship.Models.Boards;

namespace Battleship.Services;

public class Sector : ISector
{
    private readonly Coordinates _coordinates;
    public UsageType UsageType { get; set; }
    public Guid Id { get; init; }
    
    public Sector(Coordinates coordinates)
    {
        _coordinates = coordinates;
        UsageType = UsageType.Empty;
        Id = Guid.NewGuid();
    }

    public string? GetStatus()
    {
        return UsageType.GetAttribute<DescriptionAttribute>()?.Description;
    }

    public bool IsOccupied()
    {
        return UsageType is UsageType.Battleship or UsageType.Destroyer;
    }

    public Coordinates GetCoordinates()
    {
        return _coordinates;
    }
    
    public bool IsRandomAvailable()
    {
        return (_coordinates.Row % 2 == 0 && _coordinates.Column % 2 == 0)
               || (_coordinates.Row % 2 == 1 && _coordinates.Column % 2 == 1);
    }
}