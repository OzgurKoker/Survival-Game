using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public Vector2 Move => _playerInputActions.Player.Move.ReadValue<Vector2>();


    public event Action OnRunPerformed;
    public event Action OnRunCanceled;
    public event Action OnRollPerformed;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Run.performed += ctx => OnRunPerformed?.Invoke();
        _playerInputActions.Player.Run.canceled += ctx => OnRunCanceled?.Invoke();

        _playerInputActions.Player.Roll.performed += ctx => OnRollPerformed?.Invoke();
        _playerInputActions.Enable();
    }

    private void OnDestroy()
    {
        _playerInputActions.Disable();
    }
}