using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Animations : MonoBehaviour
{

    // animacion 
    public Animator animator;
    private enum MovementState { idle, running, jumping, falling}

    // cosas

    private float horizontal;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb;

    // movimiento lateral

    [SerializeField] private const float walkSpeed = 5f;
    private const float runSpeed = walkSpeed * 1.3f;
    private float moveSpeed = walkSpeed;

    // movimiento vertical

    [SerializeField] private float jumpingPower = 5f;
    private bool isFacingRight = true;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");


        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;

        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButton("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;

        }

        Run();

        Flip();

        UpdateAnimationState();

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }

        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            transform.Rotate(0f, 180f, 0f);

        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (horizontal > 0f)
        {
            state = MovementState.running;
        }
        else if (horizontal < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.01f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.01f)
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

}
