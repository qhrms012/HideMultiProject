using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public Vector2 playerVector;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private StateMachine stateMachine;
    public PhotonView pv;

    public float dieTime;
    private bool isDead = false;
    public bool isHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //stateMachine = new StateMachine();
        //stateMachine.SetState(new IdleState(stateMachine, animator));
        pv = GetComponent<PhotonView>();
    }

    public void OnMove(InputValue value)
    {
        if (pv.IsMine)
        {
            playerVector = value.Get<Vector2>();
        }       
    }

    private void Update() 
    {
        //stateMachine.Update(playerVector);
        if (isHit)
        {
            dieTime += Time.deltaTime;
        }
       
            Vector2 normalizedVector = playerVector.normalized;
        if (pv.IsMine)
        {
            if (playerVector.magnitude == 0)
            {
                SetAnimatorParameters(false, false, false, false);
            }

            if (Mathf.Abs(normalizedVector.x) > Mathf.Abs(normalizedVector.y))
            {
                if (normalizedVector.x > 0)
                    SetAnimatorParameters(true, false, false, false); // Right
                else
                    SetAnimatorParameters(false, true, false, false); // Left
            }
            else
            {
                if (normalizedVector.y > 0)
                    SetAnimatorParameters(false, false, true, false); // Up
                else if (normalizedVector.y < 0)
                    SetAnimatorParameters(false, false, false, true); // Down
            }
        }
        
    }

    private void SetAnimatorParameters(bool right, bool left, bool up, bool down)
    {
        animator.SetBool("Right", right);
        animator.SetBool("Left", left);
        animator.SetBool("Up", up);
        animator.SetBool("Down", down);
    }

    private void FixedUpdate()
    {        
        Vector2 movement = playerVector.normalized * playerSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("AI") && CompareTag("Wall"))
        //{
        //    return;
        //}
        
        if (collision.gameObject.CompareTag("Enemy") && CompareTag("Player"))
        {
            isHit = true;
            spriteRenderer.color = Color.red;
            UIManager.Instance.warningText.text = "적에게 닿고 있습니다.";
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isHit = false;
        if (CompareTag("Player"))
        {
            spriteRenderer.color = Color.white;
            UIManager.Instance.warningText.text = "적이 근처에 있습니다.";
        }
    }
}
