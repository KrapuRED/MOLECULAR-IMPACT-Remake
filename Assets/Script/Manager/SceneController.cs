using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private Scene _currentScene;

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

    public void OutDoorScene()
    {
        SceneManager.LoadScene("OutDoor");
    }

    public void WorkDayScene()
    {
        if (_currentScene.name == "WorkDay")
        {
            Debug.LogWarning("Already on WorkDay scene");
            return;
        }
        _currentScene = SceneManager.GetActiveScene();  
        SceneManager.LoadScene("WorkDay");
    }

    public void EndingScene()
    {
        SceneManager.LoadScene("EndingScene");
    }

}
