using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] TextMeshProUGUI levelCleared;
    [SerializeField] AudioSource clickSound;


    public void OptionsPanel()
    {
        levelCleared.gameObject.SetActive(false);
        Time.timeScale = 0;
        optionsPanel.SetActive(true);


    }
    public void BackToGame()
    {
        Time.timeScale = 1;
        optionsPanel.SetActive(false);

    }
    public void RestartLevel()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        optionsPanel.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        optionsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayClickSound()
    {
        clickSound.Play();
    }
}
