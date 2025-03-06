using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    // Variables de movimiento
    [SerializeField] float speed;

    [SerializeField] float jumpSpeed;
    [SerializeField] float doubleJumpSpeed;

    [SerializeField] Vector2 wallJumpForce;

    bool canDoubleJump;
    bool hasWallJumped;


    // Físicas de salto
    [SerializeField] float fallMultiplier = 2.5f;

    [SerializeField] float lowJumpMultiplier = 3f;

    // Sprite
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Animator animator;

    // Coyote time
    float coyoteTime = 0.12f;
    float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Coyote time 
        if (CheckGround.touchesGround)
        {
            coyoteTimeCounter = coyoteTime;
            hasWallJumped = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKey("space") || Input.GetKey(KeyCode.UpArrow))
        {
            if (coyoteTimeCounter > 0f)
            {
                canDoubleJump = true;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
                coyoteTimeCounter = 0f;
            }
            else
            {
                if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (CheckWall.touchesWall && !hasWallJumped)
                    {
                        hasWallJumped = true;
                        canDoubleJump = false;
                        Debug.Log("wall touch");
                        rb.linearVelocity = new Vector2(-wallJumpForce.x, wallJumpForce.y);
                    }
                    if (canDoubleJump && !hasWallJumped)
                    {
                        animator.SetBool("DoubleJump", true);
                        rb.linearVelocity = new Vector2(rb.linearVelocity.x, doubleJumpSpeed);
                        coyoteTimeCounter = 0f;
                        canDoubleJump = false;
                    }
                }
            }
        }

        if (!CheckGround.touchesGround)
        {
            animator.SetBool("Jump", true);// cambiamos a saltar
            animator.SetBool("Run", false);// quitamos correr en el aire
        }
        else
        {
            animator.SetBool("Jump", false);// quitamos saltar
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Falling", false);
        }
        if (rb.linearVelocity.y < 0)
        {
            animator.SetBool("Falling", true);
        }
        else if (rb.linearVelocity.y > 0)
        {
            animator.SetBool("Falling", false);
        }

        if (rb.linearVelocity.y < -20)
        {
            rb.transform.GetComponent<PlayerRespawn>().PlayerDamaged();

        }
    }

    void FixedUpdate()
    {

        // Teclas de movimiento
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            spriteRenderer.flipX = false; // cambiar dirección para mirar a la derecha
            animator.SetBool("Run", true); // cambiamos de idle a run
        }
        else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            spriteRenderer.flipX = true; // cambiar dirección para mirar a la izquierda
            animator.SetBool("Run", true);// cambiamos de idle a run
        }
        else
        {
            animator.SetBool("Run", false);// cambiamos de run a idle
        }

        // fisicas del salto mejoradas para que dependan de cuanto presiones el espacio
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1)) * Time.deltaTime;
        }
        if (rb.linearVelocity.y > 0 && !Input.GetKey("space") && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1)) * Time.deltaTime;
        }

        // para al jugador cuando deja de pulsar 
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.1f)
        {
            rb.linearVelocity = new Vector2(Mathf.MoveTowards(rb.linearVelocity.x, 0, 40 * Time.deltaTime), rb.linearVelocity.y);

        }
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.5f)
        {
            rb.linearVelocity = new Vector2(1 * Mathf.Sign(Input.GetAxisRaw("Horizontal")), rb.linearVelocity.y);
        }


    }

    void OnDestroy()
    {

    }

}
