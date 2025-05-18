using UnityEngine;

public class WalkState : IState
{
    private readonly PlayerController _player;
    private readonly StateMachine _stateMachine;
    private readonly PlayerAnimationController _animation;

    public WalkState(PlayerController player, StateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animation = _player.PlayerAnimationController;
    }

    public void Enter()
    {
        _animation.ChangeState(PlayerAnimState.Walk);
    }

    public void Update()
    {
        if (_player.CanRoll)
        {
            _stateMachine.ChangeState(_player.States.RollState);
        }

        if (_player.IsRunning)
        {
            _stateMachine.ChangeState(_player.States.RunState);
            return;
        }

        if (!_player.IsMoving)
        {
            _stateMachine.ChangeState(_player.States.IdleState);
            return;
        }
    }

    public void FixedUpdate()
    {
        _player.Move(_player.MoveDirection, _player.MoveSpeed);
    }

    public void Exit()
    {
    }
}