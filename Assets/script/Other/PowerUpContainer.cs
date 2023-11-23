using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpContainer : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private GameObject item;
    [SerializeField] private Animator anim;
    private void OnEnable()
    {
        anim.SetInteger("candletype", id);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("mainWeapon") || collision.gameObject.CompareTag("subWeapon"))
        {
            AudioManager.Instance.PlayGlobalSFX("Hit");
            GameObject obj = Instantiate(item);
            obj.transform.position = transform.position;
            obj.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}