using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Pooling pooling;
    public bool flip;
    public List<GameObject> Obj;
    private void OnEnable()
    {
        Spawn();
    }
    private void OnDisable()
    {
        if (Obj[0] == null)
            return;
        for(int i=0;i<Obj.Count;i++)
        {
            Obj[i].SetActive(false);
        }
        Obj.Clear();
    }
    protected virtual void Spawn()
    {
        GameObject obj = pooling.GetObject();
        obj.transform.position = this.transform.position;
        if(flip)
        {
            obj.transform.eulerAngles = new Vector3(obj.transform.position.x,180, obj.transform.position.z);
        }
        Obj.Add(obj);
        obj.SetActive(true);
    }
}
