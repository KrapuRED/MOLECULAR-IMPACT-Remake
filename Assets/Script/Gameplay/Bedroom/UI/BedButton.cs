using UnityEngine;

public class BedButton : MonoBehaviour
{
    public void OnClick()
    {
        DayCycleManager.instance.NextDay();
    }
}
