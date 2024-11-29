using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string poolName; // 오브젝트 풀 이름
        public GameObject prefab; // 풀링할 프리팹
        public int poolSize; // 초기 크기
    }

    public List<Pool> pools; // 다양한 오브젝트 풀 목록
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // 각 풀 초기화
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

    // 오브젝트 가져오기
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

    // 오브젝트 반환
    public void ReturnObject(string poolName, GameObject obj)
    {
        if (poolDictionary.ContainsKey(poolName))
        {
            obj.SetActive(false);
            poolDictionary[poolName].Enqueue(obj);
        }
    }
}

