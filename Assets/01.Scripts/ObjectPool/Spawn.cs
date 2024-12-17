using Photon.Pun;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public ObjectPoolManager poolManager;
    public UIManager uimanager;

    private void Start()
    {
        Vector3 spawnAreaCenter = new Vector3(0,0,0);
        Vector3 spawnAreaSize = new Vector3(23,30,0);
        int spawnCount = 50;
        for ( int i = 0;  i < spawnCount; i++)
        {
            SpawnObjectAtPosition("AI", spawnAreaCenter, spawnAreaSize);
            SpawnObjectAtPosition("Coin", spawnAreaCenter, spawnAreaSize);
        }
        
    }

    public GameObject SpawnObjectAtPosition(string poolName, Vector3 areaCenter, Vector3 areaSize)
    {
        GameObject obj = poolManager.GetObject(poolName);
        if (obj != null)
        {
            // 랜덤 위치 계산
            Vector3 randomPosition = new Vector3(
                Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
                Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2),
                areaCenter.z // 2D 게임이라면 Z는 고정
            );

            // 오브젝트 위치와 회전 설정
            obj.transform.position = randomPosition;
            obj.transform.rotation = Quaternion.identity; // 필요하면 특정 회전값 지정
        }
        return obj;
    }

    // 디버깅용 소환 영역 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(23, 30, 0)); // 필요 시 수정
    }
}

