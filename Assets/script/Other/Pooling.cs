using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject ObjPrefab;
    public List<GameObject> objs;
    public GameObject GetObject()
    {
        int index = objs.FindIndex(e => !e.activeInHierarchy);
        if (index >= 0)
        {
            return objs[index];
        }
        else
            return SetObject();
    }
    public GameObject SetObject()
    {
        GameObject tmp = Instantiate(ObjPrefab);
        tmp.SetActive(false);
        objs.Add(tmp);
        return tmp;
    }
    public void DelObj(GameObject obj)
    {
        objs.Remove(obj);
    }
    public void RestAll()
    {
        List<GameObject> gameobjs = new List<GameObject>(objs);
        gameobjs.RemoveAll(x => x == null);
        objs = gameobjs;
        for (int i = 0; i < objs.Count; i++)
        {
            if (objs[i].activeInHierarchy)
                objs[i].SetActive(false);
        }
    }
}
