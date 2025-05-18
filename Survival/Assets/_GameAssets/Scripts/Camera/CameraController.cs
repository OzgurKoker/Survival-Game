using System;
using Microsoft.Win32.SafeHandles;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    private CinemachineCamera _cinemachineCamera;
    private CameraInputReader _cameraInputReader;


    [Header("Zoom Settings")]
    [SerializeField] private float _zoomSpeed = 0.5f;
    [SerializeField] private float _minDistance = 1f;
    [SerializeField] private float _maxDistance = 5f;
    [SerializeField] private float _zoomSmoothTime = 0.1f;

    private float _targetZoom;
    private float _currentZoom;

    [Header("Rotation Settings")]
    [SerializeField] private float _rotationAmount = 90f;
    private Quaternion _targetRotation;
    [SerializeField] private float _rotationSmoothTime = 20f;


    private void OnEnable()
    {
        _cameraInputReader.OnRotateLeftPerformed += RotateLeft;
        _cameraInputReader.OnRotateRightPerformed += RotateRight;
        _cameraInputReader.OnZoomPerformed += Zoom;
    }

    private void OnDisable()
    {
        _cameraInputReader.OnRotateLeftPerformed -= RotateLeft;
        _cameraInputReader.OnRotateRightPerformed -= RotateRight;
        _cameraInputReader.OnZoomPerformed -= Zoom;
    }

    private void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineCamera>();
        _cameraInputReader = GetComponent<CameraInputReader>();

        _targetRotation = _cinemachineCamera.transform.rotation;
        _targetZoom = _cinemachineCamera.Lens.OrthographicSize;
        _currentZoom = _targetZoom;
    }


    private void Update()
    {
        MakeZoomSmoothness();
        MakeRotationSmoothness();
    }


    private void Zoom(Vector2 scrool)
    {
        if (Mathf.Abs(scrool.y) > 0.01f)
        {
            _targetZoom -= scrool.y * _zoomSpeed * Time.deltaTime;
            _targetZoom = Mathf.Clamp(_targetZoom, _minDistance, _maxDistance);
        }
    }

    private void MakeZoomSmoothness()
    {
        _currentZoom = Mathf.Lerp(_currentZoom, _targetZoom, _zoomSmoothTime);
        _cinemachineCamera.Lens.OrthographicSize = _currentZoom;
    }

    private void MakeRotationSmoothness()
    {
        _cinemachineCamera.transform.rotation = Quaternion.Slerp(
            _cinemachineCamera.transform.rotation,
            _targetRotation,
            Time.deltaTime * _rotationSmoothTime
        );
    }

    private void RotateLeft()
    {
        _targetRotation = Quaternion.Euler(0f, -_rotationAmount, 0f) * _targetRotation;
    }

    private void RotateRight()
    {
        _targetRotation = Quaternion.Euler(0f, _rotationAmount, 0f) * _targetRotation;
    }
}