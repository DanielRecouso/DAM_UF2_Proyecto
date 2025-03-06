using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void OnStartClick()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void OnTutorialClick()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
