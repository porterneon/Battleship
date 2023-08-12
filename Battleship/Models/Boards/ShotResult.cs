using Battleship.Enums;
using Battleship.Interfaces;

namespace Battleship.Models.Boards;

public record ShotResult(IShip? Ship, ShotStatus Status);