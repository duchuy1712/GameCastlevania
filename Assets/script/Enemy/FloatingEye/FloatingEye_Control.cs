using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEye_Control : MonoBehaviour
{
    private Collider2D target;
    private Vector2 Movement;
    [SerializeField] float Speed;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Animator anim;
    // Khoá Mục Tiêu
    public void LockTarget(Collider2D _Coll)
    {
        target = _Coll;
    }
    // Truy Bắt
    public void Chase()
    {
        // Nhìn về phía mục tiêu
        Vector3 Direction = target.bounds.center - transform.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        rb2d.rotation = angle;
        transform.position = Vector2.MoveTowards(transform.position, target.bounds.center, Speed * Time.deltaTime);
    }
}
