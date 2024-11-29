using UnityEngine;

public class Spawn : MonoBehaviour
{
    public ObjectPoolManager poolManager;

    private void Start()
    {
        Vector3 spawnAreaCenter = new Vector3(0,0,0);
        Vector3 spawnAreaSize = new Vector3(10,10,0);
        SpawnObjectAtPosition("AI", spawnAreaCenter, spawnAreaSize);
    }

    public GameObject SpawnObjectAtPosition(string poolName, Vector3 areaCenter, Vector3 areaSize)
    {
        GameObject obj = poolManager.GetObject(poolName);
        if (obj != null)
        {
            // ���� ��ġ ���
            Vector3 randomPosition = new Vector3(
                Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
                Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2),
                areaCenter.z // 2D �����̶�� Z�� ����
            );

            // ������Ʈ ��ġ�� ȸ�� ����
            obj.transform.position = randomPosition;
            obj.transform.rotation = Quaternion.identity; // �ʿ��ϸ� Ư�� ȸ���� ����
        }
        return obj;
    }
    // ���� ������Ʈ ��ȯ
    public void SpawnMultipleObjects(string poolName, Vector3 areaCenter, Vector3 areaSize, int objectCount)
    {
        for (int i = 0; i < objectCount; i++)
        {
            SpawnObjectAtPosition(poolName, areaCenter, areaSize);
        }
    }

    // ������ ��ȯ ���� ǥ��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(10, 10, 0)); // �ʿ� �� ����
    }
}

