using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    [SerializeField] Animator WeaponAnim;
    public int[] Damage;
    public void MainAttack(int weaponLv)
    {
        AudioManager.Instance.PlayUserSFX("attack");
        switch(weaponLv)
        {
            case 0:
                WeaponAnim.SetTrigger("Level1");
                break;
            case 1:
                WeaponAnim.SetTrigger("Level2");
                break;
            case 2:
                WeaponAnim.SetTrigger("Level3");
                break;
            default:
                WeaponAnim.SetTrigger("Level1");
                break;
        }
    }
}