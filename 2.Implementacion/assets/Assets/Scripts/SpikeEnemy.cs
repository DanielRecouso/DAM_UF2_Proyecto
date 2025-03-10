using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    [SerializeField] float movedownSpeed;

    [SerializeField] float moveupSpeed;
    bool movingDown = true;  // Controla si el objeto se mueve hacia abajo o hacia arriba

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Si el objeto se está moviendo hacia abajo, aplica una velocidad negativa
        if (movingDown)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -movedownSpeed);  // Mueve el objeto hacia abajo
        }
        else
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveupSpeed);  // Mueve el objeto hacia arriba
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Invertir la dirección del movimiento al chocar con otro objeto
        movingDown = !movingDown;
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerRespawn>().PlayerDamaged();
        }
    }
}


