using UnityEngine;

public class NextActivityButton : MonoBehaviour
{
    public void NextActivityButtonClicked()
    {
        GlobalEvent.OnNextActivity.Invoke();
    }
}
