using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{

    float checkpointPositionX;
    float checkpointPositionY;
    [SerializeField] Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.GetFloat("checkpointPositionX") != 0)
        {
            transform.position = new Vector2(PlayerPrefs.GetFloat("checkpointPositionX"), PlayerPrefs.GetFloat("checkpointPositionY"));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerDamaged()
    {
        animator.Play("Hit");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CheckpointReached(float x, float y)
    {
        PlayerPrefs.SetFloat("checkpointPositionX", x);
        PlayerPrefs.SetFloat("checkpointPositionY", y);
    }
}
