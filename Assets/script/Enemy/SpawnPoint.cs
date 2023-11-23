using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float cooldownTime;
    private float current_time;
    public float startupTime;
    private float startupTimeCount;
    private bool spawn;
    [SerializeField] private Animator anim;

    private void Update()
    {
        anim.SetBool("Spawn", spawn);
        if (!spawn)
        {
            if (current_time > 0)
            {
                current_time -= Time.deltaTime;
            }
            else
            {
                spawn = true;
            }
        }
        startUp();
    }
    private void startUp()
    {
        if(spawn)
        {
            if (startupTimeCount > 0)
            {
                startupTimeCount -= Time.deltaTime;
            }   
            else
            {
                current_time = cooldownTime;
                startupTimeCount = startupTime;
                Spawn("Zombie");
                spawn = false;
            }
        }
    }
    public void Spawn(string name)
    {
        GameObject obj = ObjectPooling.Instance.GetObjectFromPool(name);
        obj.transform.position = transform.position;
        obj.SetActive(true);
    }
}