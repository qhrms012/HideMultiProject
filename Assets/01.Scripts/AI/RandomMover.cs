using UnityEngine;
using System.Collections;

public class RandomMover : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private StateMachine stateMachine;

    private bool isMoving = false; // �����̴� ���� ����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
