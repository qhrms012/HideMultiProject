using UnityEngine;

public class RunState : Istate
{
    private StateMachine stateMachine;
    private Animator animator;

    public RunState(StateMachine machine, Animator animator)
    {
        stateMachine = machine;
        this.animator = animator;
    }

    public void Enter()
    {

    }

    public void Execute(Vector2 playerVector)
    {
        if(playerVector.x < 0)
        {
            animator.Play("Left");
        }
        if(playerVector.x > 0)
        {
            animator.Play("Right");
        }
        if(playerVector.y > 0)
        {
            animator.Play("Up");
        }
        if(playerVector.y < 0)
        {
            animator.Play("Down");
        }


        if(playerVector.magnitude == 0) 
        {
            stateMachine.SetState(new IdleState(stateMachine,animator));
        }
    }

    public void Exit()
    {
        Debug.Log("뛰는상태 종료");
    }
}
