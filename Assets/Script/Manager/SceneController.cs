using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GameplayScene()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void EndingScene()
    {
        SceneManager.LoadScene("EndingScene");
    }

}
