using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField] Vector2 velocidadMovimiento;
    Vector2 offset;
    Material material;
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    // Movemos el material un offset cada frame.
    // ¡Es necesario cambiar el wrap mode del sprite a repeat!
    void Update()
    {
        offset = velocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
