using UnityEngine;

public interface Istate
{
    void Enter(); // ���¿� ���Խ� ȣ��
    void Execute(Vector2 playerVector); // ���� Ȱ��ȭ �Ǿ������� �� ������ ȣ��
    void Exit(); // ���� ����� ȣ��

}
