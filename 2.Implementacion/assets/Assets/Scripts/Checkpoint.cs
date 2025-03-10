using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Si el jugador pasa por el checkpoint pasamos las coordenadas del checkpoint al metodo en PlayerRespawn
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerRespawn>().CheckpointReached(transform.position.x, transform.position.y);
            GetComponent<Animator>().enabled = true; // Activamos animación
        }
    }
}
