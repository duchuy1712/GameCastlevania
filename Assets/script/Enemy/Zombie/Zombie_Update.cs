using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class Zombie_Update : MonoBehaviour
{
    [SerializeField] Animator anim;
    public Collider2D hurtBox;
    public Transform Target;
    private void OnEnable()
    {
        TurnOffHitBox();
    }
    //State 1
    public void Rise()
    {
        anim.SetInteger("State", 1);
    }
    //State 2
    public void Attack()
    {
        anim.SetTrigger("Attack");
        anim.SetInteger("State", 3);
    }
    //State 3
    public void BackToSleep()
    {
        anim.SetInteger("State", 0);
    }
    public void TurnOnHitBox()
    {
        hurtBox.enabled = true;
    }
    public void TurnOffHitBox()
    {
        hurtBox.enabled = false;
    }
}
