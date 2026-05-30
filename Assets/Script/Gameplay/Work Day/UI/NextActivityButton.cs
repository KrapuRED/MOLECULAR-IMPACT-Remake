using UnityEngine;

public class NextActivityButton : MonoBehaviour
{
    public void NextActivityButtonClicked()
    {
        WorkDayManager.instance.ActiveWorkDay();
    }
}
