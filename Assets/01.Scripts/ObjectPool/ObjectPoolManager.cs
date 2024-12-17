using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectPoolManager : MonoBehaviourPunCallbacks, IPunPrefabPool
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
        // Photon의 Custom Pool 설정
        PhotonNetwork.PrefabPool = this;

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

    // Photon이 요청할 때 사용할 Instantiate
    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(prefabId) && poolDictionary[prefabId].Count > 0)
        {
            GameObject obj = poolDictionary[prefabId].Dequeue();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);
            return obj;
        }

        Debug.LogWarning($"Pool '{prefabId}' does not exist or is empty! Creating new instance.");
        GameObject newObj = Instantiate(Resources.Load<GameObject>(prefabId), position, rotation);
        return newObj;
    }

    // Photon이 오브젝트를 반환할 때 사용할 Destroy
    public void Destroy(GameObject gameObject)
    {
        string poolName = gameObject.name.Replace("(Clone)", "").Trim();

        if (poolDictionary.ContainsKey(poolName))
        {
            gameObject.SetActive(false);
            poolDictionary[poolName].Enqueue(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 오브젝트를 가져오는 메서드 (일반 풀링)
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
