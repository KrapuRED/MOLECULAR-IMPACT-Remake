using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public void GameStart()
    {
        SceneController.instance.GameplayScene();
    }

    public void RetryGame()
    {
        Debug.Log("HAPUS SEMUA HALNYA");
        SceneController.instance.GameplayScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
