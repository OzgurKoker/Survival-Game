using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class CameraInputReader : MonoBehaviour
{
    public event Action OnRotateLeftPerformed;
    public event Action OnRotateRightPerformed;
    public event Action<Vector2> OnZoomPerformed;

    private CameraInputActions _cameraInputActions;

    private void Awake()
    {
        _cameraInputActions = new CameraInputActions();
        _cameraInputActions.Camera.RotateLeft.performed += RotateLeftPerformed;
        _cameraInputActions.Camera.RotateRight.performed += RotateRightPerformed;
        _cameraInputActions.Camera.Zoom.performed += ZoomPerformed;
        _cameraInputActions.Camera.Enable();
    }

    private void OnDestroy()
    {
        _cameraInputActions.Camera.RotateLeft.performed -= RotateLeftPerformed;
        _cameraInputActions.Camera.RotateRight.performed -= RotateRightPerformed;
        _cameraInputActions.Camera.Zoom.performed -= ZoomPerformed;
        _cameraInputActions.Disable();
    }

    private void RotateLeftPerformed(InputAction.CallbackContext context)
    {
        OnRotateLeftPerformed?.Invoke();
    }

    private void RotateRightPerformed(InputAction.CallbackContext context)
    {
        OnRotateRightPerformed?.Invoke();
    }

    private void ZoomPerformed(InputAction.CallbackContext context)
    {
        OnZoomPerformed?.Invoke(context.ReadValue<Vector2>());
    }
}