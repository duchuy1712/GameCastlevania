using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] float DelayTime;
    public Vector3[] Pos;
    private Vector3 TargetPos;
    private bool Active;
    [SerializeField] float Speed;
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
    }
    private void Update()
    {
        if (!Active)
            return;
        if (Vector2.Distance(transform.position, TargetPos) < 0.05f)
        {
            StartCoroutine(WaitToNextPoint());
        }
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, Speed * Time.deltaTime);
    }
    IEnumerator WaitToNextPoint()
    {
        transform.position = TargetPos;
        if (PointIndex == PointCount - 1)
        {
            Direction = -1;
        }
        if (PointIndex == 0)
        {
            Direction = 1;
        }
        PointIndex += Direction;
        yield return new WaitForSeconds(DelayTime);
        TargetPos = Pos[PointIndex];
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
}
