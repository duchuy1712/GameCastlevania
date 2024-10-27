using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonB_Control : MonoBehaviour
{
    [SerializeField] float CoolDownTime,PlayerCheckRadius;
    [SerializeField] Animator anim;
    private float TimeCounter;

    [SerializeField] Pooling SpearContainer;
    [SerializeField] Transform firepoint;

    private Transform PlayerPos;
    private void Awake()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnEnable()
    {
        TimeCounter = 0;
    }
    private void OnBecameVisible()
    {
        anim.enabled = true;
    }
    private void OnDisable()
    {
        anim.enabled = false;
    }
    private void Update()
    {
        if (TimeCounter < CoolDownTime)
            TimeCounter += Time.deltaTime;
    }
    public void IdleState()
    {
        if (TimeCounter >= CoolDownTime && playercheck())
        {
            TimeCounter = 0;
            anim.SetTrigger("Attack");
        }
        if (this.transform.position.x - PlayerPos.position.x > 0)
        {
            transform.eulerAngles = Vector3.up * 180f;
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
    private bool playercheck()
    {
        LayerMask layer = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, PlayerCheckRadius, Vector2.right, 0f, layer);
        return hit.collider != null;
    }
    public void Shoot()
    {
        GameObject obj = SpearContainer.GetObject();
        obj.transform.eulerAngles = gameObject.transform.eulerAngles;
        obj.transform.position = firepoint.position;
        obj.SetActive(true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, PlayerCheckRadius);
    }
}
