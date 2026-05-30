using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeekDayEnviroment : MonoBehaviour
{
    public Image illustrastionImg;
    public TextMeshProUGUI dialogueWorkDay;

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

    public void SetIllustration(Sprite sprite, string dilaogueWork)
    {
        if (this == null) return;
        if (illustrastionImg == null) return;

        illustrastionImg.sprite = sprite;
        dialogueWorkDay.text = dilaogueWork;
    }
}
