using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEye_PlayerCheck : MonoBehaviour
{
    [SerializeField] Collider2D Coll;
    [SerializeField] Animator anim;
    [SerializeField] FloatingEye_Control Control;
    private void OnEnable()
    {
        Coll.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Control.LockTarget(collision);
            anim.SetTrigger("Chase");
            Coll.enabled = false;
        }    
    }
}
