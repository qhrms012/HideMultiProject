using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string poolName; // ������Ʈ Ǯ �̸�
        public GameObject prefab; // Ǯ���� ������
        public int poolSize; // �ʱ� ũ��
    }

    public List<Pool> pools; // �پ��� ������Ʈ Ǯ ���
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // �� Ǯ �ʱ�ȭ
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.poolName, objectPool);
        }
    }

    // ������Ʈ ��������
    public GameObject GetObject(string poolName)
    {
        if (poolDictionary.ContainsKey(poolName) && poolDictionary[poolName].Count > 0)
        {
            GameObject obj = poolDictionary[poolName].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        Debug.LogWarning($"Pool '{poolName}' does not exist or is empty!");
        return null;
    }

    // ������Ʈ ��ȯ
    public void ReturnObject(string poolName, GameObject obj)
    {
        if (poolDictionary.ContainsKey(poolName))
        {
            obj.SetActive(false);
            poolDictionary[poolName].Enqueue(obj);
        }
    }
}

