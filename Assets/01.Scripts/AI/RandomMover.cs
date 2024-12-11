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
    private bool isMoving = false; // 움직이는 상태 여부
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
            // 움직임 시작
            SetRandomDirection();
            isMoving = true;
            yield return new WaitForSeconds(Random.Range(2f, 5f)); // 2 ~ 5초 동안 이동

            // 움직임 멈춤
            isMoving = false;
            moveDirection = Vector2.zero; // 이동 방향 초기화
            yield return new WaitForSeconds(Random.Range(1f, 3f)); // 1 ~ 3초 동안 대기
        }
    }

    private void SetRandomDirection()
    {
        // 랜덤한 방향 설정
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
    }

    private void Update()
    {
        // 이동 중일 때만 StateMachine 업데이트
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
