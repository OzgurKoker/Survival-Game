using TMPro;
using UnityEngine;

public class IdleState : IState
{
    private readonly PlayerController _player;
    private readonly StateMachine _stateMachine;
    private readonly PlayerAnimationController _animation;

    public IdleState(PlayerController player, StateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animation = _player.PlayerAnimationController;

    }

    public void Enter()
    {
        _animation.ChangeState(PlayerAnimState.Idle);
    }

    public void Update()
    {
        if (_player.Input.IsRunKeyPressed && _player.IsMoving)
        {
            _stateMachine.ChangeState(_player.States.RunState);
            return;
        }

        if (_player.IsMoving)
        {
            _stateMachine.ChangeState(_player.States.WalkState);
            return;
        }
    }

    public void FixedUpdate()
    {
    }

    public void Exit()
    {
    }
}