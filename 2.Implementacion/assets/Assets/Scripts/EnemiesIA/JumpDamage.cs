using UnityEngine;

public class JumpDamage : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] GameObject destroyParticles;

    float jumpForce = 6f;

    int lifes = 2;

    // Cuando el jugador toca el collider en la cabeza del enemigo le hace daño y rebota.
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * jumpForce;
            LoseLifeAndHit();
            CheckLife();
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {

    }

    void LoseLifeAndHit()
    {
        lifes--;
        animator.Play("Hit");
    }
    // Cuando el enemigo no tiene vidas se hace una animación al morir y luego se destruye
    void CheckLife()
    {
        if (lifes == 0)
        {
            destroyParticles.SetActive(true);
            spriteRenderer.enabled = false;
            Invoke("EnemyDie", 0.2f);
        }
    }

    void EnemyDie()
    {
        Destroy(gameObject);
    }

}
