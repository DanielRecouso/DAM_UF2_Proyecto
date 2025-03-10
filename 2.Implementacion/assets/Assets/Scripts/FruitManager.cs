using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitManager : MonoBehaviour
{
    public static FruitManager Instance;

    // private int appleCount;
    // private int strawberryCount;

    [SerializeField] TextMeshProUGUI collectedFruits;
    [SerializeField] TextMeshProUGUI totalFruits;
    [SerializeField] TextMeshProUGUI levelCleared;

    bool isLevelClearedActivated = false;
    int totalFruitsInLevel;
    private void Awake()
    {
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
        totalFruitsInLevel = transform.childCount;
        // CountFruits();
    }

    void Update()
    {
        AllFruitsCollected();
        totalFruits.text = totalFruitsInLevel.ToString();
        collectedFruits.text = (totalFruitsInLevel - transform.childCount).ToString();
    }
    // private void CountFruits()
    // {
    //     appleCount = GameObject.FindGameObjectsWithTag("Apple").Length;
    //     strawberryCount = GameObject.FindGameObjectsWithTag("Strawberry").Length;
    // }

    // public void FruitCollected(string tag)
    // {
    //     switch (tag)
    //     {
    //         case "Apple":
    //             appleCount--;
    //             if (appleCount <= 0) Debug.Log("¡Todas las manzanas recogidas!");
    //             break;
    //         case "Strawberry":
    //             strawberryCount--;
    //             if (strawberryCount <= 0) Debug.Log("¡Todas las fresas recogidas!");
    //             break;
    //     }
    // }

    //Si terminamos las frutas y estamos en un nivel que no sea el tutorial o el último, cambiamos de escena.
    public void AllFruitsCollected()
    {
        if (transform.childCount == 0 && SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 3)
        {
            levelCleared.gameObject.SetActive(true);
            isLevelClearedActivated = false;
            Invoke("ChangeScene", 1);
        }
        else if (transform.childCount == 0 && SceneManager.GetActiveScene().buildIndex == 1 && !isLevelClearedActivated)
        {
            PlayerPrefs.DeleteAll();
            levelCleared.gameObject.SetActive(true);
            isLevelClearedActivated = true;
        }
        else if (transform.childCount == 0 && SceneManager.GetActiveScene().buildIndex == 3)
        {
            PlayerPrefs.DeleteAll();
            levelCleared.gameObject.SetActive(true);
        }
    }

    // Funcion para cambiar de escena eliminando la posición del checkpoint
    void ChangeScene()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
