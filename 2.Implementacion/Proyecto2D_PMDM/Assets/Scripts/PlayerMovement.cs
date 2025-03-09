using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator animator;

    // Movement variables
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpSpeed = 12f;
    [SerializeField] private float doubleJumpSpeed = 10f;
    private bool canDoubleJump = false;
    private bool hasDoubleJumped = false;

    // Wall sliding variables
    private bool isWallSliding;
    [SerializeField] private float wallSlidingSpeed = 2f;

    // Physics variables
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 3f;

    // Ground and wall detection
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;

    // Coyote time and jump buffering
    private float coyoteTime = 0.12f;
    private float coyoteTimeCounter;

    // Facing direction
    private bool isFacingRight = true;

    [SerializeField] GameObject optionsPanel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsPanel.SetActive(true);
            Time.timeScale = 0;
        }

        HandleCoyoteTime();
        HandleJump();
        HandleAnimations();
        WallSlide();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJumpPhysics();
    }

    private void HandleCoyoteTime()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            canDoubleJump = true;
            hasDoubleJumped = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (coyoteTimeCounter > 0f && !isWallSliding)
            {
                Jump(jumpSpeed);
                coyoteTimeCounter = 0f;
            }
            else if (canDoubleJump && !hasDoubleJumped && !isWallSliding)
            {
                Jump(doubleJumpSpeed);
                hasDoubleJumped = true;
            }
        }
    }

    private void Jump(float jumpForce)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        animator.SetBool("Jump", true);
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(horizontalInput * speed, rb.linearVelocity.y);

        if ((horizontalInput > 0f && !isFacingRight) || (horizontalInput < 0f && isFacingRight))
        {
            Flip();
        }

        if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            rb.linearVelocity = new Vector2(Mathf.MoveTowards(rb.linearVelocity.x, 0, 40 * Time.deltaTime), rb.linearVelocity.y);
        }
    }

    private void HandleJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey("space") && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void HandleAnimations()
    {
        animator.SetBool("Run", Mathf.Abs(rb.linearVelocity.x) > 0.1f && IsGrounded());
        animator.SetBool("Jump", !IsGrounded() && rb.linearVelocity.y > 0.1f);
        animator.SetBool("Falling", !IsGrounded() && rb.linearVelocity.y < -0.1f);
        animator.SetBool("DoubleJump", hasDoubleJumped);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0f)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlidingSpeed);
            animator.SetBool("Slide", true);
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("Slide", false);
        }
    }
}