using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    public List<GameObject> listObjectToPool = new List<GameObject>();
    public int size;
    Dictionary<string, List<GameObject>> poolDict = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        for (int j = 0; j < listObjectToPool.Count; j++)
            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(listObjectToPool[j], transform);
                obj.SetActive(false);
                string key = listObjectToPool[j].name;

                if (!poolDict.ContainsKey(key))
                {
                    poolDict.Add(key, new List<GameObject>());
                }
                poolDict[key].Add(obj);
            }
    }
    public GameObject GetObjectFromPool(string key) // "Bullet"
    {
        if (poolDict.ContainsKey(key))
        {
            for (int i = 0; i < poolDict[key].Count; i++)
            {
                int temp = i;
                if (!poolDict[key][temp].activeInHierarchy)
                {
                    return poolDict[key][temp];
                }
            }
            int _index = -1;
            for (int i = 0; i < listObjectToPool.Count; i++)
            {
                if (listObjectToPool[i].name.Equals(key))
                {
                    _index = i;
                    break;
                }
            }
            GameObject newObj = Instantiate(listObjectToPool[_index], transform);
            poolDict[key].Add(newObj);
            return newObj;
        }
        return null;
    }
}
