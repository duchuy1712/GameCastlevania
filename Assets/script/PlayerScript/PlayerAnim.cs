using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public bool IsAttacking => anim.GetCurrentAnimatorStateInfo(0).IsName("GroundAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("AirAttack");
    [SerializeField] private Animator anim ;
    [SerializeField] private SpriteRenderer sprite;
    private string current_st;
    private void LateUpdate()
    {
        anim.SetBool("Stun", PlayerManager.Instance.Stat.stun);
        NormalState();
    }
    public void DeathAnimation()
    {
        anim.SetTrigger("Death");
    }
    public void AttackState()
    {
        if(PlayerManager.Instance.ConTrol.Grounded())
        {
            anim.SetTrigger("GroundAttack");
        }
        else
        {
            anim.SetTrigger("JumpAttack");
        }
    }
    private void NormalState()
    {
        if (PlayerManager.Instance.ConTrol.Grounded())
        {
            if (!PlayerManager.Instance.ConTrol.running)
                anim.SetInteger("State", 0);
            else if (PlayerManager.Instance.ConTrol.running)
                anim.SetInteger("State", 1);
        }
        else
        {
            anim.SetInteger("State", 2);
        }
        Color tmp = sprite.color;
        if (PlayerManager.Instance.Stat.immortal.Equals(true) && PlayerManager.Instance.Stat.stun.Equals(false))
        {
            tmp.a = 0.2f;
        }
        else
        {
            tmp.a = 1f;
        }
        sprite.color = tmp;
    }
}
