using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void OnStartClick()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("FirstLevel");
    }
    public void OnTutorialClick()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Tutorial");
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
}
