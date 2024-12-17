using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectPoolManager : MonoBehaviourPunCallbacks, IPunPrefabPool
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
        // Photon�� Custom Pool ����
        PhotonNetwork.PrefabPool = this;

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

    // Photon�� ��û�� �� ����� Instantiate
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

    // Photon�� ������Ʈ�� ��ȯ�� �� ����� Destroy
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

    // ������Ʈ�� �������� �޼��� (�Ϲ� Ǯ��)
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
