using UnityEngine;
using UnityEngine.UI;

public class WeekDayEnviroment : MonoBehaviour
{
    public Image illustrastionImg;

    private void OnEnable()
    {
        GlobalEvent.OnShowIllustrastionWorkDay.Addistener(SetIllustration);
    }

    private void OnDisable()
    {
        RemoveListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

    private void RemoveListener()
    {
        GlobalEvent.OnShowIllustrastionWorkDay.Removeistener(SetIllustration);

    }

    public void SetIllustration(Sprite sprite)
    {
        if (this == null) return;
        if (illustrastionImg == null) return;

        illustrastionImg.sprite = sprite;
    }
}
