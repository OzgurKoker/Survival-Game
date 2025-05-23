using UnityEngine;

public class RunState : IState
{
    private readonly PlayerController _player;
    private readonly StateMachine _stateMachine;
    private readonly PlayerAnimationController _animation;

    public RunState(PlayerController player, StateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animation = _player.PlayerAnimationController;
    }

    public void Enter()
    {
        _animation.ChangeState(PlayerAnimState.Run);
    }

    public void Update()
    {
        if (_player.CanRoll)
        {
            _stateMachine.ChangeState(_player.States.RollState);
        }

        if (!_player.IsRunning && _player.IsMoving)
        {
            _stateMachine.ChangeState(_player.States.WalkState);
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
    }

    public void Exit()
    {
    }
}