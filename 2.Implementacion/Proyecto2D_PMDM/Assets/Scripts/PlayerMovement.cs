using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed;

    [SerializeField] float jumpSpeed;

    Rigidbody2D rb;

    [SerializeField] float fallMultiplier = 2.5f;

    [SerializeField] float lowJumpMultiplier = 3f;

    float coyoteTime = 0.1f;
    float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

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
        }
        else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }
        if ((Input.GetKey("space") || Input.GetKey(KeyCode.UpArrow)) && coyoteTimeCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            coyoteTimeCounter = 0f;
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
