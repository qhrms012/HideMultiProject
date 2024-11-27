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
        if (playerVector.magnitude == 0) 
        { 
            stateMachine.SetState(new IdleState(stateMachine,animator));
            return;
        }
        Vector2 normalizedVector = playerVector.normalized;

        if(Mathf.Abs(normalizedVector.x) > Mathf.Abs(normalizedVector.y))
        {
            animator.Play(normalizedVector.x > 0 ? "Right" : "Left");
        }
        else
        {
            animator.Play(normalizedVector.y > 0 ? "Up" : "Down");
        }

    }

    public void Exit()
    {
        Debug.Log("뛰는상태 종료");
    }
}
