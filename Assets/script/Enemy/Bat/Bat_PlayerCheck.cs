using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_PlayerCheck : MonoBehaviour
{
    [SerializeField] Collider2D Coll;
    [SerializeField] Bat_Control Control;
    [SerializeField] Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Control.SteupDirection(collision.transform.position);
            Coll.enabled = false;
            anim.SetTrigger("Fly");
        }
    }
    private void OnEnable()
    {
        Coll.enabled = true;
    }
}
