using NUnit.Framework.Interfaces;
using UnityEngine;

public class IdleState : Istate
{
    private StateMachine stateMachine;
    private Animator animator;

    public IdleState(StateMachine machine, Animator animator)
    {
        stateMachine = machine;
        this.animator = animator;
    }

    public void Enter()
    {
        animator.Play("Idle");
    }

    public void Execute(Vector2 playerVector)
    {
        if (playerVector.magnitude > 0)
        {
            stateMachine.SetState(new RunState(stateMachine, animator));
        }
    }

    public void Exit()
    {

    }
}
