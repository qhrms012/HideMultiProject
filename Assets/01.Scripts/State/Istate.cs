using UnityEngine;

public interface Istate
{
    void Enter(); // 상태에 진입시 호출
    void Execute(Vector2 playerVector); // 상태 활성화 되어있을시 매 프레임 호출
    void Exit(); // 상태 종료시 호출

}
