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

    public float dieTime;
    private bool isDead = false;
    public bool isHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (isHit)
        {
            dieTime += Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {        
        Vector2 movement = playerVector.normalized * playerSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && CompareTag("Player"))
        {
            isHit = true;
            spriteRenderer.color = Color.red;
            UIManager.Instance.warningText.text = "적에게 닿고 있습니다.";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CompareTag("Player"))
        {
            isHit = false;
            spriteRenderer.color = Color.white;
            UIManager.Instance.warningText.text = "적이 근처에 있습니다.";
        }
    }

}
