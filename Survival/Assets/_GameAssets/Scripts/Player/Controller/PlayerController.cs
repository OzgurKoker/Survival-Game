using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private IPlayerInput _playerInput;
    private StateMachine _stateMachine;
    private PlayerAnimationController _playerAnimationController;
    private Rigidbody _rigidbody;
    public PlayerStateContainer States { get; private set; }

    [SerializeField] private Transform _cameraTransform;


    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _speedMultiplier = 1.3f;
    [SerializeField] private float _rotationSpeed = 10f;


    #region Publics

    public bool IsMoving => _playerInput.Move.sqrMagnitude > 0.01f;
    public bool CanRoll => _playerInput.IsRollKeyPressed && IsMoving;
    public IPlayerInput Input => _playerInput;
    public PlayerAnimationController PlayerAnimationController => _playerAnimationController;
    public float MoveSpeed => _playerInput.IsRunKeyPressed ? _moveSpeed * _speedMultiplier : _moveSpeed;
    public Vector3 MoveDirection
    {
        get
        {
            Vector2 input = _playerInput.Move;
            Vector3 inputDir = new Vector3(input.x, 0f, input.y);
            Vector3 moveDirection = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * inputDir;
            moveDirection.Normalize();
            return moveDirection;
        }
    }

    #endregion


    private void Awake()
    {
        _playerInput = GetComponent<IPlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
        _rigidbody.freezeRotation = true;

        _stateMachine = new StateMachine();
        States = new PlayerStateContainer(this, _stateMachine);
        _stateMachine.ChangeState(States.IdleState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }


    public void Move(Vector3 moveDirection, float speed)
    {
        Vector3 velocity = moveDirection * speed;
        Vector3 newPosition = _rigidbody.position + velocity * Time.fixedDeltaTime;
        _rigidbody.MovePosition(newPosition);
        RotateTowards(moveDirection, _rotationSpeed);
    }

    private void RotateTowards(Vector3 moveDirection, float rotationSpeed)
    {
        if (moveDirection == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation,
            rotationSpeed * Time.fixedDeltaTime));
    }
}