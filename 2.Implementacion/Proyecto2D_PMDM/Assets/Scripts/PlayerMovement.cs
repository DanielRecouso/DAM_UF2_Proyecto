using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    // Variables de movimiento
    [SerializeField] float speed;

    [SerializeField] float jumpSpeed;

    // Físicas de salto
    [SerializeField] float fallMultiplier = 2.5f;

    [SerializeField] float lowJumpMultiplier = 3f;

    // Sprite
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Animator animator;

    // Coyote time
    float coyoteTime = 0.11f;
    float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        // Coyote time 
        if (CheckGround.touchesGround)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
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
        if ((Input.GetKey("space") || Input.GetKey(KeyCode.UpArrow)) && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            coyoteTimeCounter = 0f;
        }

        if (!CheckGround.touchesGround)
        {
            animator.SetBool("Jump", true);// cambiamos a saltar
            animator.SetBool("Run", false);// quitamos correr en el aire
        }
        else
        {
            animator.SetBool("Jump", false);// quitamos saltar
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
            rb.linearVelocity = new Vector2(Mathf.MoveTowards(rb.linearVelocity.x, 0, 20 * Time.deltaTime), rb.linearVelocity.y);

        }

    }
    void Update()
    {

    }

}
