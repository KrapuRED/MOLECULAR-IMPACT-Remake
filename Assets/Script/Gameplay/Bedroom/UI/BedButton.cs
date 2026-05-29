using UnityEngine;

public class BedButton : MonoBehaviour
{
    public void OnClick()
    {
        GlobalEvent.OnNextDay.Invoke();
        GameManager.instance.NextGamePlay();
    }
}
