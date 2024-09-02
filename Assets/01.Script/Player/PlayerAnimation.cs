using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Idle,
    Move,
    JumpUP,
    JumpDown,
    Attack,
    Dash,
    Sit,
    Hit,
    Die
}
public class PlayerAnimation : MonoBehaviour
{
    public States currentState;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        switch(currentState)
        {
            case States.Idle:
                animator.SetBool("Move", false);
                break;
            case States.Move:
                animator.SetBool("Move", true);
                break;
            case States.JumpUP:
                animator.SetTrigger("JumpUP");
                break;
            case States.JumpDown:
                animator.SetTrigger("JumpDown");
                break;
            case States.Attack:
                animator.SetTrigger("Attack");
                break;
            case States.Dash:
                animator.SetTrigger("Dash");
                break;
            case States.Sit:
                animator.SetTrigger("Sit");
                break;
            case States.Hit:
                animator.SetTrigger("Hit");
                break;
            case States.Die:
                animator.SetTrigger("Die");
                break;
        }
    }
}
