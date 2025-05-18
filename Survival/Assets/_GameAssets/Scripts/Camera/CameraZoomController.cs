using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    [Header("References")]
    private IPlayerInput _playerInput;
    private CinemachineCamera _cinemachineCamera;


    [Header("Zoom Settings")]
    [SerializeField] private float _zoomSpeed = 0.5f;
    [SerializeField] private float _minDistance = 1f;
    [SerializeField] private float _maxDistance = 5f;

    [SerializeField] private float _smoothTime = 0.1f;
    
    private float _targetZoom;
    private float _currentZoom;

    private void Awake()
    {
        _cinemachineCamera = GetComponent<CinemachineCamera>();
        _playerInput = GetComponent<IPlayerInput>();

        _targetZoom = _cinemachineCamera.Lens.OrthographicSize;
        _currentZoom = _targetZoom;
    }

    private void Update()
    {
        float scrollY = _playerInput.Scrool.y;


        if (Mathf.Abs(scrollY) > 0.01f)
        {
            _targetZoom -= scrollY * _zoomSpeed * Time.deltaTime;
            _targetZoom = Mathf.Clamp(_targetZoom, _minDistance, _maxDistance);
        }

        _currentZoom = Mathf.Lerp(_currentZoom, _targetZoom, _smoothTime);
        _cinemachineCamera.Lens.OrthographicSize = _currentZoom;
    }
}