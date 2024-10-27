using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonA_Control : MonoBehaviour
{
    [SerializeField] Transform CheckPoint,HitBoxPos;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] int Direction = 1;
    [SerializeField] float GroundCheckRange, WallCheckRange,PlayerCheckRadius,Speed,CoolDownTime;
    private LayerMask GroundLayer => LayerMask.GetMask("Ground");
    private LayerMask PlayerLayer => LayerMask.GetMask("Player");
    private int CurrentDirection;
    private float TimeCounter;
    private void OnBecameVisible()
    {
        anim.enabled = true;
    }
    private void OnDisable()
    {
        anim.enabled = false;
    }
    // Reset Direction and CoolDown
    private void OnEnable()
    {
        TimeCounter = CoolDownTime;
        CurrentDirection = Direction;
        if (Direction > 0)
        {
            transform.eulerAngles = Vector2.zero;
        }
        else
        {
            transform.eulerAngles = Vector3.up * 180f;
        }
    }
    //Attack CoolDown
    private void Update()
    {
        if (TimeCounter < CoolDownTime)
            TimeCounter += Time.deltaTime;
    }
    public void MovingState()
    {
        // Moving
        rb.velocity = new Vector2(Speed * CurrentDirection * Time.deltaTime, rb.velocity.y);
        AttackCheck();
        // Direction
        if (!GroundCheck() || !WallCheck())
        {
            if (CurrentDirection > 0)
            {
                CurrentDirection = -1;
                transform.eulerAngles = Vector3.up * 180f;
            }
            else
            {
                CurrentDirection = 1;
                transform.eulerAngles = Vector2.zero;
            }
        }
    }
    public void AttackCheck()
    {
        if (TimeCounter >= CoolDownTime && PlayerCheck())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetTrigger("Attack");
            TimeCounter = 0;
        }
    }
    private bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(CheckPoint.position, Vector2.down, GroundCheckRange, GroundLayer);
        return hit.collider != null;
    } 
    private bool WallCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(CheckPoint.position, Vector2.right, WallCheckRange * Direction, GroundLayer);
        return hit.collider == null;
    }
    private bool PlayerCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(HitBoxPos.position, PlayerCheckRadius, Vector2.right,0f, PlayerLayer);
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(CheckPoint.position, Vector2.down * GroundCheckRange);
        Gizmos.DrawRay(CheckPoint.position, Vector2.right * WallCheckRange * Direction);
        Gizmos.DrawWireSphere(HitBoxPos.position , PlayerCheckRadius);
    }
}
