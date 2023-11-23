using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool running => !xAxis.Equals(0);
    [Header("Moving Stat")]
    public int MovingSpeed;
    private float xAxis;
    [Header("Jumping Stat")]
    [SerializeField] float JumpHeigh;
    [SerializeField] float jumpGravityScale;
    [SerializeField] float fallGravityScale;
    private float jumpForce => Mathf.Sqrt(JumpHeigh * (Physics2D.gravity.y * rb2d.gravityScale) * -2) * rb2d.mass;
    public float coyoteTime = 0;
    public float jumpBuffer = 0;
    [Header("PlayerPhysic")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Collider2D coll;
    [SerializeField] private LayerMask layer;
    //special jump effect
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private bool MovingCondition => ((PlayerManager.Instance.Stat.stun.Equals(true) || PlayerManager.Instance.Animation.IsAttacking) && Grounded()) || PlayerManager.Instance.Stat.Reset.Equals(true);
    private void FixedUpdate()
    {
        if (PlayerManager.Instance.Stat.Reset.Equals(true))
        {
            xAxis = 0;
            rb2d.gravityScale = 0f;
            return;
        }
        moving();
        Jumping();
    }
    private void Update()
    {
        if (Time.timeScale.Equals(0))
            return;
        GetInput();
        if (xAxis > 0)
            transform.eulerAngles = Vector3.zero;
        else if (xAxis < 0)
            transform.eulerAngles = Vector3.up * 180f;
    }
    private void moving()
    {
        rb2d.velocity = new Vector2(xAxis * MovingSpeed * Time.deltaTime, rb2d.velocity.y);
    }
    private void GetInput()
    {
        if (!MovingCondition)
            xAxis = Input.GetAxis("Horizontal");
        else
            xAxis = 0;
        if (Input.GetKeyDown(KeyCode.Space) && !PlayerManager.Instance.Animation.IsAttacking)
            jumpBufferCounter = jumpBuffer;
        else
            jumpBufferCounter -= Time.deltaTime;
        if (Grounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
    }
    private void Jumping()
    {
        if (rb2d.velocity.y > 0)
        {
            rb2d.gravityScale = jumpGravityScale;
        }
        else
        {
            rb2d.gravityScale = fallGravityScale;
        }
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb2d.gravityScale = jumpGravityScale;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            jumpBufferCounter = -1;
            coyoteTimeCounter = -1;
        }
    }
    public bool Grounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(rb2d.position, coll.bounds.size - new Vector3(0.1f, 0.5f, 0f), 0f, Vector2.down, 0.5f, layer);
        return hit.collider != null;
    }
}
