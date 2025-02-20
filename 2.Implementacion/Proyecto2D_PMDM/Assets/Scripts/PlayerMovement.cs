using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float speed;

    [SerializeField] float jumpSpeed;

    Rigidbody2D rb;

    [SerializeField] float fallMultiplier = 2.5f;

    [SerializeField] float lowJumpMultiplier = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        // Teclas de movimiento
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
        }
        else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
        }
        if ((Input.GetKey("space") || Input.GetKey(KeyCode.UpArrow)) && CheckGround.touchesGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }

        // fisicas del salto mejoradas para que dependan de cuanto presiones el espacio
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1)) * Time.deltaTime;
        }
        if (rb.linearVelocity.y > 0 && !Input.GetKey("space"))
        {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1)) * Time.deltaTime;
        }
        //
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.1f)
        {
            rb.linearVelocity = new Vector2(Mathf.MoveTowards(rb.linearVelocity.x, 0, 20 * Time.deltaTime), rb.linearVelocity.y);

        }

    }
    void Update()
    {

    }

}
