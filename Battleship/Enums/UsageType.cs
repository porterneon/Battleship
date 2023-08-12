using System.ComponentModel;

namespace Battleship.Enums;

public enum UsageType
{
    [Description("o")]
    Empty,
    [Description("B")]
    Battleship,
    [Description("D")]
    Destroyer,
    [Description("X")]
    Hit,
    [Description("-")]
    Miss
}