using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string ItemType;
    public int value;
    private int CountDown = 3;
    private float cur_time = 0;
    
    private void OnEnable()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);
    }
    private void OnDisable()
    {
        cur_time = 0;
    }
    private void Update()
    {
        cur_time += Time.deltaTime;
        if(cur_time >= CountDown)
        {
            Destroy(gameObject);
        }
    }
}
