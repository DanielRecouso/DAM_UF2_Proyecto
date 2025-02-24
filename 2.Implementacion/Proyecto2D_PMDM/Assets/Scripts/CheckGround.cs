using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool touchesGround;
    [SerializeField] LayerMask groundLayer;


    // Comprueba con que layer esta colisionando el GroundChecker
    // En caso de que colisiones con la layer Ground puede saltar
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            touchesGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            touchesGround = false;
        }
    }
}
