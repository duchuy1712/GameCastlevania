using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackRange : MonoBehaviour
{
    [SerializeField] Zombie_Update zombie;
    [SerializeField] Animator anim;
    [SerializeField] float attackCooldown;
    private bool StateCheck => anim.GetCurrentAnimatorStateInfo(0).IsName("Walking") || anim.GetCurrentAnimatorStateInfo(0).IsName("Wait 0");
    public float CoolDownCounter;
    private void OnEnable()
    {
        CoolDownCounter = attackCooldown;
    }
    private void Update()
    {
        if(CoolDownCounter < attackCooldown)
        {
            CoolDownCounter += Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && StateCheck)
        {
            if (CoolDownCounter >= attackCooldown)
            {
                CoolDownCounter = 0;
                zombie.Attack();
            }
            else
                anim.SetInteger("State", 3);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            zombie.Rise();
        }
    }
}
