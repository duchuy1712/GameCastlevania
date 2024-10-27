using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MovementControl : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] BoxCollider2D coll;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Animator animator;
    [Header("Player Stat")]
    [SerializeField] float MovingSpeed;
    [SerializeField] float jumpGravityScale;
    [SerializeField] float jumpheight;
    [SerializeField] float fallGravityScale;
    private int Flip = 0;
    public int xAxis { private set; get; }
    private bool IsAttacking => animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    // ngan nhan vat di chuyen khi thuc hien tan cong tren mat dat
    private int OtherForce => System.Convert.ToInt32(!(IsAttacking && GroundCheck()));
    [Header("ChangeColliderSize")]
    [SerializeField] float jumpColliderPercentOfHeigh = 0.5f;
    private Vector2 standColliderSize;
    private Vector2 standColliderOffSet;
    private Vector2 jumpColliderSize;
    private Vector2 jumpColliderOffset;
    [Header("Ground Check")]
    [SerializeField] float castDistance;
    [SerializeField] Vector3 BoxSize;
    [Header("Jump Effect")]
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    public float coyoteTime = 0.1f;
    public float jumpBuffer = 0.1f;
    [Header("Script")]
    [SerializeField] PlayerInput Input;
    // Di chuyển trên Platform
    public bool IsOnPlatform;
    public Rigidbody2D PlatformRB;
    private LayerMask layer => LayerMask.GetMask("Ground");
    // thay đổi hitbox khi đứng ở mặt đất và ở trên không
    private void Awake()
    {
        //set hitbox size for each state
        // onGroundState
        standColliderSize = coll.size;
        standColliderOffSet = coll.offset;
        // !onGroundState
        jumpColliderSize = new Vector2(standColliderSize.x, standColliderSize.y * jumpColliderPercentOfHeigh);
        jumpColliderOffset = new Vector2(standColliderOffSet.x, standColliderOffSet.y * jumpColliderPercentOfHeigh);
    }
    private void FixedUpdate()
    {
        moving();
        Jumping();
    }
    private void Update()
    {
        // thay đổi hướng của nhân vật
        if (IsAttacking || Flip.Equals(xAxis))
            return;
        if (xAxis != Flip)
        {
            if (xAxis > 0)
                transform.eulerAngles = Vector3.zero;
            else if (xAxis < 0)
                transform.eulerAngles = Vector3.up * 180f;
            Flip = xAxis;
        }
    }
    private void LateUpdate()
    {
        Animate();
    }
    // Di chuyển
    private void moving()
    {
        if (IsOnPlatform)
        {
            rb2d.velocity = new Vector2((xAxis * OtherForce * MovingSpeed * Time.deltaTime) + PlatformRB.velocity.x, PlatformRB.velocity.y);
        }
        else
            rb2d.velocity = new Vector2(xAxis * OtherForce * MovingSpeed * Time.deltaTime, rb2d.velocity.y);
    }
    // Nhảy
    private void Jumping()
    {
        // Coyote và Jump Buffer
        if (jumpBufferCounter > 0)
            jumpBufferCounter -= Time.deltaTime;
        if (GroundCheck())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
        // Jump
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            //IsOnPlatform = false;
            ChangeGravity(jumpGravityScale);
            // tính lực cần cho object để giữ object luôn nhảy đến độ cao mong muốn
            float jumpForced = Mathf.Sqrt(jumpheight * (Physics2D.gravity.y * rb2d.gravityScale) * -2) * rb2d.mass;
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForced);
            jumpBufferCounter = 0;
            // Check input de fix bug do M.Jump va Jump Buffer gay ra
            if (!Input.actions.FindAction("Jump").IsPressed())
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
                coyoteTimeCounter = 0;
                ChangeGravity(fallGravityScale);
            }
        }
        // thay doi trong luc khi player roi xuong
        if (rb2d.velocity.y < 0)
            ChangeGravity(fallGravityScale);
    }
    // Check mặt đất
    public bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(coll.bounds.center.x, coll.bounds.min.y), new Vector2(coll.bounds.size.x - BoxSize.x, BoxSize.y), 0, -transform.up, castDistance, layer);
        if (hit.collider)
        {
            coll.size = standColliderSize;
            coll.offset = standColliderOffSet;
        }
        else
        {
            coll.size = jumpColliderSize;
            coll.offset = jumpColliderOffset;

        }
        return hit.collider != null;
    }
    // Thay đổi trọng lực
    public void ChangeGravity(float _Gravity)
    {
        rb2d.gravityScale = _Gravity;
    }
    // Input System
    public void MoveInput(InputAction.CallbackContext ctxt)
    {
        xAxis = (int)ctxt.ReadValue<float>();
    }
    public void JumpInput(InputAction.CallbackContext ctxt)
    {
        if (IsAttacking)
            return;
        if (ctxt.started)
            jumpBufferCounter = jumpBuffer;
        if (ctxt.canceled && rb2d.velocity.y > 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            coyoteTimeCounter = 0;
            ChangeGravity(fallGravityScale);
        }
    }
    // Animation
    private void Animate()
    {
        if (GroundCheck())
        {
            if (xAxis.Equals(0))
                animator.SetInteger("State", 0);
            else
                animator.SetInteger("State", 1);
        }
        else
        {
            animator.SetInteger("State", 2);
        }
    }
}
