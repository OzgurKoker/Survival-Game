using System;
using UnityEngine;
using UnityEngine.Rendering.UI;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [Header("References")]
    private Animator _animator;
    private PlayerAnimState _currentState;

    
    

    #region Publics

   
    public AnimatorStateInfo StateInfo => _animator.GetCurrentAnimatorStateInfo(0);


    #endregion

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _currentState = PlayerAnimState.Idle;
    }

    public void ChangeState(PlayerAnimState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;

        ResetAllAnimatorBools();

        switch (newState)
        {
            case PlayerAnimState.Idle:
                _animator.SetBool(Consts.PlayerAnimations.IS_IDLE, true);
                _animator.SetBool(Consts.PlayerAnimations.IS_WALKING, false);
                _animator.SetBool(Consts.PlayerAnimations.IS_RUNNING, false);
                break;

            case PlayerAnimState.Walk:
                _animator.SetBool(Consts.PlayerAnimations.IS_IDLE, false);
                _animator.SetBool(Consts.PlayerAnimations.IS_WALKING, true);
                _animator.SetBool(Consts.PlayerAnimations.IS_RUNNING, false);
                break;

            case PlayerAnimState.Run:
                _animator.SetBool(Consts.PlayerAnimations.IS_IDLE, false);
                _animator.SetBool(Consts.PlayerAnimations.IS_WALKING, false);
                _animator.SetBool(Consts.PlayerAnimations.IS_RUNNING, true);
                break;
            case PlayerAnimState.Roll:
                _animator.SetTrigger(Consts.PlayerAnimations.ROLL_TRIGGER);
                break;
        }
    }


    private void ResetAllAnimatorBools()
    {
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsRunning", false);
        _animator.ResetTrigger("RollTrigger");
    }
}