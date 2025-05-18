using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private PlayerInputReader _playerInputReader;
    private StateMachine _stateMachine;
    private PlayerAnimationController _playerAnimationController;
    private Rigidbody _rigidbody;
    public PlayerStateContainer States { get; private set; }

    [SerializeField] private Transform _cameraTransform;


    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _speedMultiplier = 1.3f;
    [SerializeField] private float _rotationSpeed = 10f;


    private bool _isRunning = false;
    private bool _canRoll = false;


    #region Publics

    public bool IsMoving => _playerInputReader.Move.sqrMagnitude > 0.01f;
    public bool CanRoll
    {
        get => _canRoll;
        set => _canRoll = value;
    }
    public bool IsRunning => _isRunning;
    public PlayerInputReader Input => _playerInputReader;
    public PlayerAnimationController PlayerAnimationController => _playerAnimationController;
    public float MoveSpeed => _isRunning ? _moveSpeed * _speedMultiplier : _moveSpeed;
    public Vector3 MoveDirection => CalculateMoveDirection();

    #endregion


    private void OnEnable()
    {
        _playerInputReader.OnRunPerformed += () => _isRunning = true;
        _playerInputReader.OnRunCanceled += () => _isRunning = false;
        _playerInputReader.OnRollPerformed += () =>
        {
            if (IsMoving && !_canRoll)
                _canRoll = true;
        };
    }

    private void OnDisable()
    {
        //TODO:Cancel Events
    }

    private void Awake()
    {
        _playerInputReader = GetComponent<PlayerInputReader>();
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

    private Vector3 CalculateMoveDirection()
    {
        Vector2 input = _playerInputReader.Move;
        Vector3 inputDir = new Vector3(input.x, 0f, input.y);
        Vector3 moveDirection = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * inputDir;
        return moveDirection.normalized;
    }

    private void RotateTowards(Vector3 moveDirection, float rotationSpeed)
    {
        if (moveDirection == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation,
            rotationSpeed * Time.fixedDeltaTime));
    }
}