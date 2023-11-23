using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public bool auto;
    [SerializeField] Pooling FlowerBudsContain;
    [SerializeField] Animator anim;
    [SerializeField] GameObject firepoint;
    private bool Grownning;
    public float reGrownTime;
    private float reGrownTimeCount;
    public float range;
    public LayerMask playerLayer;

    private void Update()
    {
        if(Grownning)
        {
            if (reGrownTimeCount > 0)
            {
                anim.SetInteger("State", 1);
                reGrownTimeCount -= Time.deltaTime;
            }
            else
            {
                anim.SetTrigger("Grwonning");
                Grownning = false;
            }
        }
        else
        {
            anim.SetInteger("State", 0);
            if((check() || auto.Equals(true)) && anim.GetCurrentAnimatorStateInfo(0).IsName("FullGrown"))
            {
                reGrownTimeCount = reGrownTime;
                Grownning = true;
                Shoot();
            }
        }    
            
    }
    private bool check()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, range,playerLayer);
        return hit.collider != null;
    }
    private void Shoot()
    {
        GameObject obj = FlowerBudsContain.GetObject();
        obj.transform.position = firepoint.transform.position;
        obj.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(gameObject.transform.position, Vector2.down * range);
    }
}
