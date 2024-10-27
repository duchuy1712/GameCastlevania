using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private Pooling item;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {
            AudioManager.Instance.PlayGlobalSFX("Hit");
            Spawn();
            gameObject.SetActive(false);
        }
    }
    private void Spawn()
    {
        GameObject obj = item.GetObject();
        obj.transform.position = this.transform.position;
        obj.SetActive(true);
    }
}