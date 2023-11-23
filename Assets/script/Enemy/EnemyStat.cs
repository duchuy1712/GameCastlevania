using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [SerializeField] private EnemySO Stat;
    private int hp;
    private int damage;
    private int point;
    private float StunCountdown;
    public bool IsStunning { get; private set; }

    private void Awake()
    {
        hp = Stat.hp;
        damage = Stat.damge;
        point = Stat.point;
    }
    private void Update()
    {
        if(IsStunning.Equals(true))
        {
            if (StunCountdown > 0)
            {
                StunCountdown -= Time.deltaTime;
            }
            else
            {
                IsStunning = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("mainWeapon"))
        {
            hurt(PlayerManager.Instance.AttackControl.mainDamge);
        } 
        else if(collision.gameObject.CompareTag("subWeapon"))
        {
            hurt(PlayerManager.Instance.AttackControl.subDamage);
        }
        if((collision.gameObject.CompareTag("mainWeapon") || collision.gameObject.CompareTag("subWeapon")) && IsStunning.Equals(false))
        {
            StunCountdown = Stat.StunTime;
            IsStunning = true;
        }
    }
    private void hurt(int _amount)
    {
        hp -= _amount;
        AudioManager.Instance.PlayGlobalSFX("Hit");
        if (hp <= 0)
        {
            GameObject obj = ObjectPooling.Instance.GetObjectFromPool("Hit");
            obj.transform.position = this.gameObject.transform.position;
            obj.SetActive(true);
            gameObject.SetActive(false);
            PlayerManager.Instance.Stat.GetPoint(point);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerManager.Instance.Stat.hurt(damage);
        }
    }
}
