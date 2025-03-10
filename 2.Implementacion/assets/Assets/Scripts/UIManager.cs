using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] TextMeshProUGUI levelCleared;
    [SerializeField] AudioSource clickSound;


    // Menu al clickar en opciones
    public void OptionsPanel()
    {
        levelCleared.gameObject.SetActive(false);
        Time.timeScale = 0;
        optionsPanel.SetActive(true);


    }

    // Vovler al juego
    public void BackToGame()
    {
        Time.timeScale = 1;
        optionsPanel.SetActive(false);

    }

    // Reiniciar nivel
    public void RestartLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        optionsPanel.SetActive(false);
    }

    // Volver al menu principal
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        optionsPanel.SetActive(false);
    }

    // Salir del juego
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayClickSound()
    {
        clickSound.Play();
    }
}
