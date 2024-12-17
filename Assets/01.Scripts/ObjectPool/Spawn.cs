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
        // ���� ��ġ ���
        Vector3 randomPosition = new Vector3(
            Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
            Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2),
            areaCenter.z // 2D �����̶�� Z�� ����
        );

        // PhotonNetwork�� ���� ������Ʈ ����
        if (PhotonNetwork.IsMasterClient) // MasterClient�� ��ȯ�ϵ��� ���� �߰�
        {
            GameObject obj = PhotonNetwork.Instantiate(prefabName, randomPosition, Quaternion.identity);
            Debug.Log($"{prefabName} ��ȯ��: ��ġ {randomPosition}");
        }
        else
        {
            Debug.LogWarning("SpawnObjectAtPosition: MasterClient�� ��ü�� ��ȯ�� �� �ֽ��ϴ�.");
        }
    }

    // ������ ��ȯ ���� ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(23, 30, 0)); // �ʿ� �� ����
    }
}

