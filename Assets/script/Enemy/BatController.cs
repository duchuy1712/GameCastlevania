using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatController : MonoBehaviour
{
    [SerializeField] AiFlyEnemy AiControl;
    public float range;
    public LayerMask player;
    private float curTime = 0;
    public bool attack { get; private set; }

    private void Update()
    {
        if(check())
        {
            attack = true;
        }
        if(attack == true)
        {
            curTime += Time.deltaTime;
            if (curTime >= 0.1f)
            {
                AiControl.UpdatePath();
                curTime = 0;
            }
        }

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
