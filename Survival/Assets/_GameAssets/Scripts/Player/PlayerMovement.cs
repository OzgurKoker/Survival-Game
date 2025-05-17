using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private IPlayerInput _playerInput;
    private Rigidbody _rigidbody;

    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 5f;
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
        CheckGround();
        HandleMovement();
        HandleJump();
    }


    private void CheckGround()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        float rayLength = _groundCheckDistance;
        Debug.DrawRay(rayOrigin, Vector3.down * rayLength, Color.red);
        _isGrounded = Physics.Raycast(rayOrigin, Vector3.down, rayLength, _groundMask);
    }


    private void HandleMovement()
    {
        Vector2 moveInput = _playerInput.Move;
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        Vector3 moveVelocity = moveDirection.normalized * _moveSpeed;

        Vector3 newPosition = _rigidbody.position + moveVelocity * Time.fixedDeltaTime;
        _rigidbody.MovePosition(newPosition);
    }

    private void HandleJump()
    {
        if (_playerInput.IsJumpPressed && _isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}