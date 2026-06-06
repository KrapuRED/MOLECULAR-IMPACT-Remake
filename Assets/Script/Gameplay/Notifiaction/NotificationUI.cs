using UnityEngine;
using TMPro;

public class NotificationUI : MonoBehaviour
{
    [Header("Upper Part")]
    [SerializeField] private TMP_Text titleEffectText;

    [Header("Midle Part")]
    [SerializeField] private TMP_Text descriptionEffectText;

    [Header("Bottom Part")]
    [SerializeField] private TMP_Text effectsText;

    public void SetUpNotification(PerkSO perkData)
    {
        titleEffectText.text = perkData.perkName;
        descriptionEffectText.text = perkData.perkDescription;

        effectsText.text = "";

        for (int i = 0; i < perkData.perkEffect.Length; i++)
        {
            PerkEffect effect = perkData.perkEffect[i];
            effectsText.text += $"{effect.effectName}\n";
        }
    }

    public void OnButtonClick()
    {
        Destroy(gameObject);
    }
}
