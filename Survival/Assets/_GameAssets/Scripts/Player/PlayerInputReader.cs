using System;
using UnityEngine;

public class PlayerInputReader : MonoBehaviour, IPlayerInput
{
    private PlayerInputActions _playerInputActions;

    public Vector2 Move => _playerInputActions.Player.Move.ReadValue<Vector2>();
    public bool IsJumpPressed => _playerInputActions.Player.Jump.IsPressed();
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