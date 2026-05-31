using UnityEngine;

public class BackMapButton : MonoBehaviour
{
    public void OnBackMapButtonClick()
    {
        SceneController.instance.OutDoorScene();
    }
}
