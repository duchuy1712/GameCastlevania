using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemySO Stat;
    [SerializeField] Collider2D AttackBox;
    [SerializeField] bool Invicible;
    private int hp;
    private float StunCountdown;
    private bool IsStunning;
    private void OnEnable()
    {
        hp = Stat.hp;
        if(AttackBox != null)
            AttackBox.enabled = false;
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
        if(!Invicible && collision.gameObject.CompareTag("PlayerWeapon") && !IsStunning)
        {
            hurt(collision.gameObject.GetComponent<WeaponData>().data.damage);
            StunCountdown = Stat.StunTime;
            IsStunning = true;
            if (hp <= 0)
            {
                GameUI.Instance.GetPoint(Stat.point);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
    private void hurt(int _amount)
    {
        hp -= _amount;
        AudioManager.Instance.PlayGlobalSFX("Hit");
    }
}
