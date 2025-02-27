using UnityEngine;

public class FruitCollected : MonoBehaviour
{
    //Cuando el jugador entre en contacto con la fruta: 
    // La fruta desaparece, la animación empieza y después de 0.5s desaparece el gameobject
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            FruitManager.Instance.FruitCollected(gameObject.tag);
            Destroy(gameObject, 0.5f);
        }

    }
}
