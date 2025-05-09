using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class PoolData
    {
        public string poolKey; //프리팹 구분
        public GameObject obstaclePrefab; //장애물 프리팹
        public int poolSize = 10; //생성할 오브젝트의 갯수
    }

    [SerializeField] private List<PoolData> poolConfigs;
    
    private Dictionary<string, Queue<GameObject>> poolDictionary=new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string,GameObject> prefabDictionary=new Dictionary<string, GameObject>();


    public void InitializePool(Transform parent)
    {
        foreach (var pool in poolConfigs)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.obstaclePrefab,parent);
                obj.SetActive(false);
                obj.GetComponent<PoolableObject>().PoolKey = pool.poolKey;
                obj.GetComponent<PoolableObject>().PoolManager=this;
                objectQueue.Enqueue(obj);
            }
            poolDictionary[pool.poolKey] = objectQueue;
            prefabDictionary[pool.poolKey] = pool.obstaclePrefab;
        }
    }

    public GameObject GetPoolObject(string _poolKey, Transform parent)
    {
        if(!poolDictionary.ContainsKey(_poolKey)) return null;
        
        var objectQueue = poolDictionary[_poolKey];

        if (objectQueue.Count > 0)
        {
            GameObject obj = objectQueue.Dequeue();
            obj.transform.SetParent(parent);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefabDictionary[_poolKey],parent);             
            obj.GetComponent<PoolableObject>().PoolKey = _poolKey;
            obj.GetComponent<PoolableObject>().PoolManager=this;
            return obj;
        }
    }

    public void ReturnPoolObject(GameObject _obj,  string _poolKey)
    {
        _obj.SetActive(false);
        poolDictionary[_poolKey].Enqueue(_obj);
    }
}
