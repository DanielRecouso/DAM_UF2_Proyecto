using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float speed;
    float waitTime;

    [SerializeField] Transform[] moveSpots;
    [SerializeField] float startWaitTime = 2f;
    int i = 0;
    Vector2 actualPos;

    void Start()
    {
        waitTime = startWaitTime;
    }


    void Update()
    {
        // Movemos al enemigo a una posición que ya hemos determinado en moveSpots.
        StartCoroutine(CheckEnemyMoving());
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);

        // Si ya ha llegado al punto,  esperamos el "waitTime" y cambiamos el punto al siguiente si no es el últmo.
        if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 0.1f)
        {

            if (waitTime <= 0f)
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    // Comprobamos cada 0.5s si se está moviendo, y en que direción se está moviendo.
    // En caso de que la posición en x sea la misma ( está parado ) se pone el Idle.
    IEnumerator CheckEnemyMoving()
    {
        actualPos = transform.position;

        yield return new WaitForSeconds(0.5f);

        if (transform.position.x > actualPos.x)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("Idle", false);
        }
        else if (transform.position.x < actualPos.x)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("Idle", false);
        }
        else if (transform.position.x == actualPos.x)
        {
            animator.SetBool("Idle", true);
        }
    }
}
