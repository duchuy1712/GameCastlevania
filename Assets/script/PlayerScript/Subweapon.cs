using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subweapon : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    public Rigidbody2D rb2d;
    private Vector2 direction;
    public Vector2 basedirec;
    public void FixedUpdate()
    {
        rb2d.velocity = new Vector2(direction.x, direction.y) * weaponData.data.speed*Time.deltaTime;
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
    public void SetDirection(Vector2 _direction)
    {
        direction = basedirec;
        rb2d.gravityScale = 0;
        transform.eulerAngles = _direction;
        if(transform.eulerAngles.y > 0)
            direction = new Vector2(-direction.x, direction.y);
    }
}
