using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCannon : MonoBehaviour
{
    public bool auto;
    [SerializeField] Pooling CannonBallContain;
    [SerializeField] Animator anim;
    [SerializeField] GameObject firepoint;
    public float range;
    public float ReloadTime;
    private float ReloadTimeCount;
    public LayerMask player;

    private void Update()
    {
        if((check() || auto.Equals(true)) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            if (ReloadTimeCount > 0)
            {
                ReloadTimeCount -= Time.deltaTime;
            }
            else
            {
                anim.SetTrigger("Fire");
                ReloadTimeCount = ReloadTime;
            }
        }
        else
        {
            ReloadTimeCount = ReloadTime;
        }    
    }
    private void Shoot()
    {
        GameObject obj = CannonBallContain.GetObject();
        obj.transform.eulerAngles = gameObject.transform.eulerAngles;
        obj.transform.position = firepoint.transform.position;
        obj.SetActive(true);
    }
    private bool check()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, range, player);
        return hit != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
