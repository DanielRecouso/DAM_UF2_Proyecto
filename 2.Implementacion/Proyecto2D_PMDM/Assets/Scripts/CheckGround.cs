using UnityEngine;

public class CheckGround : MonoBehaviour
{
    public static bool touchesGround;
    void Start()
    {

    }
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        touchesGround = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        touchesGround = false;
    }
}
