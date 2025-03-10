using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{

    float checkpointPositionX;
    float checkpointPositionY;
    [SerializeField] Animator animator;
    void Start()
    {
        // Comprobamos si tiene algún float con ese nombre guardado.
        // En caso de que lo tenga lleva al jugador a esa posición.
        if (PlayerPrefs.GetFloat("checkpointPositionX") != 0)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("checkpointPositionX"), PlayerPrefs.GetFloat("checkpointPositionY"));
        }
    }
    void Update()
    {

    }

    // Si hacen daño al jugador se reinicia la escena (en caso de haber tocado checkpoint reincia desde el checkpoint).
    public void PlayerDamaged()
    {
        animator.Play("Hit");
        Invoke("ResetScene", 0.1f);

    }

    void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Cuando toca el checkpoint asignamos 2 floats al jugador para guardar su checkpoint.
    public void CheckpointReached(float x, float y)
    {
        PlayerPrefs.SetFloat("checkpointPositionX", x);
        PlayerPrefs.SetFloat("checkpointPositionY", y);
    }
}
