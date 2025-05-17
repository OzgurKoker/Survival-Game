using UnityEngine;

public interface IPlayerInput
{
    Vector2 Move { get; }
    bool IsJumpPressed { get; }
}