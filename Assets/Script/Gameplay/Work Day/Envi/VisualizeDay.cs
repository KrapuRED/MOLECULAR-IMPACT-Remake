using UnityEngine;
using UnityEngine.UI;

public class VisualizeDay : MonoBehaviour
{
    [SerializeField] private Days visualizeDay;
    [SerializeField] private Image backgroundImg;

    private void OnEnable()
    {
        GlobalEvent.OnUpdateVisualizeDay.Addistener(OnVisualizeDay);
    }

    private void OnDisable()
    {   
        OnRemoveListener();
    }

    private void OnDestroy()
    {
        OnRemoveListener();
    }

    private void OnRemoveListener()
    {

        GlobalEvent.OnUpdateVisualizeDay.Removeistener(OnVisualizeDay);
    }

    public void OnVisualizeDay(Days currentDay)
    {
        if (this == null) return;  

        if (currentDay == visualizeDay)
        {
            backgroundImg.color = Color.green;
        }
        else
        {
            backgroundImg.color = Color.white;

        }
    }
}
