using UnityEngine;

public class FruitManager : MonoBehaviour
{
    public static FruitManager Instance;

    private int appleCount;
    private int strawberryCount;

    private void Awake()
    {
        // Singleton básico
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CountFruits();
    }

    private void CountFruits()
    {
        appleCount = GameObject.FindGameObjectsWithTag("Apple").Length;
        strawberryCount = GameObject.FindGameObjectsWithTag("Strawberry").Length;
    }

    public void FruitCollected(string tag)
    {
        switch (tag)
        {
            case "Apple":
                appleCount--;
                if (appleCount <= 0) Debug.Log("¡Todas las manzanas recogidas!");
                break;
            case "Strawberry":
                strawberryCount--;
                if (strawberryCount <= 0) Debug.Log("¡Todas las fresas recogidas!");
                break;
        }
    }
}
