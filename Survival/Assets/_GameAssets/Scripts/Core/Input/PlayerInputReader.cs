using System;
using System.Linq;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour, IPlayerInput
{
    private PlayerInputActions _playerInputActions;

    public Vector2 Move => _playerInputActions.Player.Move.ReadValue<Vector2>();
    public bool IsRunKeyPressed => _playerInputActions.Player.Run.IsPressed();
    public bool IsRollKeyPressed => _playerInputActions.Player.Roll.IsPressed();

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
    }

    private void OnDestroy()
    {
        _playerInputActions.Disable();
    }
}