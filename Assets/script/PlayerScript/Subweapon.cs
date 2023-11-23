using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subweapon : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb2d;
    private Vector2 direction;
    public Vector2 basedirec;
    private void OnEnable()
    {
        direction = basedirec;
        rb2d.gravityScale = 0;
        if (PlayerManager.Instance.transform.eulerAngles.Equals(Vector3.zero))
        {
            direction = new Vector2(direction.x, direction.y);
        }
        else if (PlayerManager.Instance.transform.eulerAngles.Equals(Vector3.up * 180))
        {
            direction = new Vector2(-direction.x, direction.y);
        }
    }
    private void Update()
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;
        if (direction.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = Vector3.up * 180;
        }
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Candle"))
        {
            gameObject.SetActive(false);
        }
    }
}
