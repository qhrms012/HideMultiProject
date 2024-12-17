using Photon.Pun;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //public ObjectPoolManager poolManager;
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

    public void SpawnObjectAtPosition(string prefabName, Vector3 areaCenter, Vector3 areaSize)
    {
        // 랜덤 위치 계산
        Vector3 randomPosition = new Vector3(
            Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
            Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2),
            areaCenter.z // 2D 게임이라면 Z는 고정
        );

        // PhotonNetwork를 통해 오브젝트 생성
        if (PhotonNetwork.IsMasterClient) // MasterClient만 소환하도록 조건 추가
        {
            GameObject obj = PhotonNetwork.Instantiate(prefabName, randomPosition, Quaternion.identity);
            Debug.Log($"{prefabName} 소환됨: 위치 {randomPosition}");
        }
        else
        {
            Debug.LogWarning("SpawnObjectAtPosition: MasterClient만 객체를 소환할 수 있습니다.");
        }
    }

    // 디버깅용 소환 영역 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(23, 30, 0)); // 필요 시 수정
    }
}

