using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Idle,
    Move,
    Jump,
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
        }
    }
}
