using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float damage;
    public float speed;
    private float curtime;
    public float time;

    public bool isDone = false;
    private Vector2 direction;
    public void Setup(float _damage, Vector2 dir)
    {
        damage = speed;
        direction = dir;
        isDone = true;
    }
    private void Update()
    {
        if (!isDone) return;
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        if (direction.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = Vector3.up * 180;
        }
        curtime += Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("PlayerTag"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
