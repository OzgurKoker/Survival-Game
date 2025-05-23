using TMPro;
using UnityEngine;

public class RollState : IState
{
    private readonly PlayerController _player;
    private readonly StateMachine _stateMachine;
    private readonly PlayerAnimationController _animation;


    public RollState(PlayerController player, StateMachine stateMachine)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animation = _player.PlayerAnimationController;
    }

    public void Enter()
    {
        _animation.ChangeState(PlayerAnimState.Roll);
        _player.CanRoll = false;
    }

    public void Update()
    {
        if (_animation.StateInfo.IsName(Consts.PlayerAnimations.PlayerAnimatorClipNames.ROLL_ANIM) &&
            _animation.StateInfo.normalizedTime >= 1f)
        {
            if (_player.IsRunning)
            {
                _stateMachine.ChangeState(_player.States.RunState);
                return;
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
    }

    public void FixedUpdate()
    {
    }

    public void Exit()
    {
    }
}