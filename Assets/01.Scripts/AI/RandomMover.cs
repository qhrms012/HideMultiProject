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

        // �⺻ ���� ���� (IdleState �Ǵ� ������ ����)
        stateMachine.SetState(new IdleState(stateMachine, animator));
    }

    private void Start()
    {
        SetRandomDirection();
        InvokeRepeating(nameof(SetRandomDirection), 5f, 5f); // 2�ʸ��� ���� ���� ����
    }

    private void SetRandomDirection()
    {
        // ���� ���� ����
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
    }

    private void Update()
    {
        stateMachine.Update(moveDirection); // ���� ����
    }

    private void FixedUpdate()
    {
        Vector2 movement = moveDirection.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
