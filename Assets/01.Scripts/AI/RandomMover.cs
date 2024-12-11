using UnityEngine;
using System.Collections;

public class RandomMover : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private StateMachine stateMachine;
    private SpriteRenderer spriteRenderer;

    private float dieTime;
    private bool isMoving = false; // �����̴� ���� ����
    private bool isHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        stateMachine = new StateMachine();
        stateMachine.SetState(new IdleState(stateMachine, animator));
    }

    private void Start()
    {
        StartCoroutine(MoveAndPauseRoutine());
    }

    private IEnumerator MoveAndPauseRoutine()
    {
        while (true)
        {
            // ������ ����
            SetRandomDirection();
            isMoving = true;
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // 2 ~ 5�� ���� �̵�

            // ������ ����
            isMoving = false;
            moveDirection = Vector2.zero; // �̵� ���� �ʱ�ȭ
            yield return new WaitForSeconds(Random.Range(1f, 3f)); // 1 ~ 3�� ���� ���
        }
    }

    private void SetRandomDirection()
    {
        // ������ ���� ����
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
    }

    private void Update()
    {
        // �̵� ���� ���� StateMachine ������Ʈ
        if (isMoving)
        {
            stateMachine.Update(moveDirection);
        }

        if (isHit)
        {
            dieTime += Time.deltaTime;

            if(dieTime >= 5f && isHit)
            {
                gameObject.SetActive(false);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isHit = true;
            spriteRenderer.color = Color.red;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isHit = false;
            spriteRenderer.color = Color.white;
        }
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            Vector2 movement = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}
