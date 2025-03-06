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
    void Update()
    {
        offset = velocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
