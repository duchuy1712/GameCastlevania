using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_OutRange : MonoBehaviour
{
    [SerializeField] Zombie_Update zombie;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            zombie.BackToSleep();
        }
    }
}
