using System;
using UnityEngine;
using UnityEngine.Rendering.UI;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private PlayerAnimState _currentState;


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
                _animator.SetBool("IsIdle", true);
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", false);
                break;

            case PlayerAnimState.Walk:
                _animator.SetBool("IsIdle", false);
                _animator.SetBool("IsWalking", true);
                _animator.SetBool("IsRunning", false);
                break;

            case PlayerAnimState.Run:
                _animator.SetBool("IsIdle", false);
                _animator.SetBool("IsWalking", false);
                _animator.SetBool("IsRunning", true);
                break;
        }
    }


    private void ResetAllAnimatorBools()
    {
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsRunning", false);
    }
}