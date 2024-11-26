using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public Vector2 playerVector;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private StateMachine stateMachine;


    private bool isDead = false;
    private bool isHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine();
        stateMachine.SetState(new IdleState(stateMachine, animator));
    }

    public void OnMove(InputValue value)
    {
        playerVector = value.Get<Vector2>();
    }

    private void Update() 
    {
        stateMachine.Update(playerVector);
    }

    private void FixedUpdate()
    {        
        Vector2 movement = playerVector.normalized * playerSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


}
