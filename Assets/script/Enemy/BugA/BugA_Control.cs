using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugA_Control : MonoBehaviour
{
    [SerializeField] Pooling Ammo;
    [SerializeField] Animator Anim;
    [SerializeField] Transform CheckPos;
    [SerializeField] Transform FirePoint;
    [SerializeField] float GroundRayLength = 1f;
    [SerializeField] float WallRayLength = 1f;
    [SerializeField] float RayLength;
    [SerializeField] float Speed;
    [SerializeField] float CoolDownTime;

    private LayerMask GroundLayer => LayerMask.GetMask("Ground");
    private LayerMask PlayerLayer => LayerMask.GetMask("Player");
    [SerializeField] int Direction = 1;
    private float CoolDownTimeCount;

    private void OnEnable()
    {
        CoolDownTimeCount = CoolDownTime;
    }
    private void Update()
    {
        // CoolDownTime Attack
        if (CoolDownTimeCount < CoolDownTime)
            CoolDownTimeCount += Time.deltaTime;
    }
    // Moving
    public void Moving()
    {
        //Flip
        if (!GroundCheck()||!WallCheck())
        {
            Direction = -Direction;
        }
        if (Direction > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
            transform.eulerAngles = Vector3.up * 180;
        //Move
        transform.Translate(Vector2.right * Speed * Direction * Time.deltaTime,Space.World);
        // Trigger Attack
        if (CoolDownTimeCount >= CoolDownTime && PlayerCheck())
        {
            CoolDownTimeCount = 0;
            Anim.SetTrigger("Attack");
        }
    }    
    // GroundCheck WallCheck PlayerCheck
    private bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(CheckPos.position, Vector2.up, GroundRayLength, GroundLayer);
        return hit.collider != null;
    }
    private bool WallCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(CheckPos.position, Vector2.right * Direction, WallRayLength , GroundLayer);
        return hit.collider == null;
    }
    private bool PlayerCheck()
    {
        RaycastHit2D Ray = Physics2D.Raycast(CheckPos.position, Vector2.down, RayLength, PlayerLayer);
        return Ray.collider != null;
    }
    // Stop and Attack
    public void Shoot()
    {
        GameObject obj = Ammo.GetObject();
        obj.transform.eulerAngles = gameObject.transform.eulerAngles;
        obj.transform.position = FirePoint.transform.position;
        obj.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(CheckPos.position, WallRayLength * Vector2.right * Direction);
        Gizmos.DrawRay(CheckPos.position, RayLength * Vector2.down);
        Gizmos.DrawRay(CheckPos.position, GroundRayLength * Vector2.up);
    }
}
