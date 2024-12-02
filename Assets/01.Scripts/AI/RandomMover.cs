using UnityEngine;

public class RandomMover : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private StateMachine stateMachine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine();

        // 기본 상태 설정 (IdleState 또는 적절한 상태)
        stateMachine.SetState(new IdleState(stateMachine, animator));
    }

    private void Start()
    {
        SetRandomDirection();
        InvokeRepeating(nameof(SetRandomDirection), 5f, 5f); // 2초마다 랜덤 방향 갱신
    }

    private void SetRandomDirection()
    {
        // 랜덤 방향 설정
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
    }

    private void Update()
    {
        stateMachine.Update(moveDirection); // 방향 전달
    }

    private void FixedUpdate()
    {
        Vector2 movement = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
