using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovingPlatform : MonoBehaviour
{
    public Vector3[] Pos;
    private Vector3 TargetPos;
    private bool Active;
    [SerializeField] float Speed;
    [SerializeField] float WaitTime;
    [SerializeField] Rigidbody2D rb2d;
    Vector2 MoveDirection;
    int PointIndex;
    int PointCount;
    int Direction = 1;

    private void OnBecameVisible()
    {
        Active = true;
    }
    private void OnEnable()
    {
        PointIndex = 0;
        PointCount = Pos.Length;
        transform.position = Pos[0];
        TargetPos = Pos[1];
        DirectionCaculate();
    }
    private void Update()
    {
        if (!Active)
            return;
        if(Vector2.Distance(transform.position,TargetPos) < 0.05f)
        {
            NextPoint();
        }
    }
    private void FixedUpdate()
    {
        
        if (!Active)
            return;
        rb2d.velocity = MoveDirection * Speed * Time.deltaTime ;
    }// tính toán hướng di chuyển đến vị trí tiếp theo
    private void DirectionCaculate()
    {
        MoveDirection = (TargetPos - transform.position).normalized;
    }
    // Đổi targetpos Sang vị trí tiếp theo
    void NextPoint()
    {
        transform.position = TargetPos;
        MoveDirection = Vector2.zero;
        if (PointIndex == PointCount - 1)
        {
            Direction = -1;
        }
        if (PointIndex == 0)
        {
            Direction = 1;
        }
        PointIndex += Direction;
        TargetPos = Pos[PointIndex];
        StartCoroutine(WaitToNextPoint());
    }
    IEnumerator WaitToNextPoint()
    {
        yield return new WaitForSeconds(WaitTime);
        DirectionCaculate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MovementControl PlayerMoving = collision.gameObject.GetComponent<MovementControl>();
            PlayerMoving.IsOnPlatform = true;
            PlayerMoving.PlatformRB = rb2d;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MovementControl PlayerMoving = collision.gameObject.GetComponent<MovementControl>();
            PlayerMoving.IsOnPlatform = false;
            PlayerMoving.PlatformRB = null;
        }
    }
    private void OnDisable()
    {
        Active = false;
        StopAllCoroutines();
    }
    private void OnDrawGizmos()
    {
        if (Pos.Length == 0)
            return;
        for (int i = 0; i < Pos.Length; i++)
        {
            Gizmos.DrawWireSphere(Pos[i], 1f);
        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus && Vector2.Distance(transform.position, TargetPos) < 2f)
        {
            NextPoint();
        }
    }
}
