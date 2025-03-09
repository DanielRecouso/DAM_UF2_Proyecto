using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerRespawn>().CheckpointReached(transform.position.x, transform.position.y);
            GetComponent<Animator>().enabled = true;
        }
    }
}
