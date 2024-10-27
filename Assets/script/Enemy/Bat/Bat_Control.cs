using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Control : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int VerticalRange;
    [SerializeField] int HorizontalRange;
    private int H_Direction;
    private int V_Direction;
    private Vector2 Point;
    private LayerMask PlayerLayer => LayerMask.GetMask("Player");
    public void Move()
    {
        ChangeV_Direction();
        transform.position = Vector2.MoveTowards(transform.position,Point,speed * Time.deltaTime);
    }
    public void ChangeV_Direction()
    {
        if (Vector2.Distance(transform.position,Point).Equals(0))
        {
            V_Direction = -V_Direction;
            Point = new Vector2(transform.position.x + (HorizontalRange * H_Direction), transform.position.y + (VerticalRange * V_Direction));
        }
    }
    private void OnEnable()
    {
        Point = transform.position;
    }
    public void SteupDirection(Vector2 Position)
    {
        if (transform.position.x - Position.x > 0)
        {
            H_Direction = -1;
            V_Direction = 1;
            transform.eulerAngles = Vector3.up * 180f;
        }
        else
        {
            H_Direction = 1;
            V_Direction = 1;
            transform.eulerAngles = Vector3.zero;
        }
    }
}
