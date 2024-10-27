using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePlayerCheck : MonoBehaviour
{
    [SerializeField] Zombie_Update zombie;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            zombie.Rise();
            zombie.Target = collision.gameObject.transform;
        }
    }
}
