using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private IPlayerInput _playerInput;
    private Rigidbody _rigidbody;
    [SerializeField] private Transform _cameraTransform;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _speedMultiplier = 1.5f;

    [SerializeField] private float _jumpForce = 2.5f;
    [SerializeField] private float _groundCheckDistance = 0.3f;
    [SerializeField] private LayerMask _groundMask;


    private bool _isGrounded;

    private void Awake()
    {
        _playerInput = GetComponent<IPlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        //CheckGround();
        HandleMovement();
        //HandleJump();
    }


    private void HandleMovement()
    {
        Vector2 moveInput = _playerInput.Move;

        Vector3 inputDir = new Vector3(moveInput.x, 0f, moveInput.y);

        Vector3 moveDirection = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * inputDir;
        moveDirection.Normalize();

        float speedMultiplier = _playerInput.IsRunKeyPressed ? _speedMultiplier : 1f;

        Vector3 moveVelocity = moveDirection * (_moveSpeed * speedMultiplier);
        Vector3 newPosition = _rigidbody.position + moveVelocity * Time.fixedDeltaTime;

        _rigidbody.MovePosition(newPosition);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, 10f * Time.fixedDeltaTime));
        }
    }

    private void CheckGround()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        float rayLength = _groundCheckDistance;
        Debug.DrawRay(rayOrigin, Vector3.down * rayLength, Color.red);
        _isGrounded = Physics.Raycast(rayOrigin, Vector3.down, rayLength, _groundMask);
    }

    private void HandleJump()
    {
        if (_playerInput.IsJumpPressed && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}