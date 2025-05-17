using UnityEngine;

public interface IPlayerInput
{
    Vector2 Move { get; }
    bool IsRunKeyPressed { get; }
    bool IsRollKeyPressed { get; }
}