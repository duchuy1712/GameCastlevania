using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject camera_room;
    [SerializeField] private GameObject Enemy_in_room;
    [SerializeField] private Collider2D RoomColl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            camera_room.SetActive(true);
            if (Enemy_in_room != null)
                Enemy_in_room.SetActive(true);
            else
                return;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            camera_room.SetActive(false);
            RoomColl.isTrigger = false;
            if (Enemy_in_room != null)
                Enemy_in_room.SetActive(false);
            else
                return;
        }
    }
}
