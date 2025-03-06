using UnityEngine;

public class CheckWall : MonoBehaviour
{
    public static bool touchesWall;
    [SerializeField] LayerMask groundLayer;


    // Comprueba con que layer esta colisionando el GroundChecker
    // En caso de que colisiones con la layer Ground puede saltar
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            touchesWall = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            touchesWall = false;
        }
    }
}
