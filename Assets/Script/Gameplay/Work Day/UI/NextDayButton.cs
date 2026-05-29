using UnityEngine;

public class NextDayButton : MonoBehaviour
{
    public void OnNextDayButtonClicked()
    {
        GlobalEvent.OnNextDayWorkDay.Invoke();
        GameManager.instance.NextGamePlay();
    }
}
