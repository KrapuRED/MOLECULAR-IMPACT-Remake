using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowEffectsContentUI : MonoBehaviour
{
    [SerializeField] private Image perkIcon;
    
    [Header("Upper Part")]
    [SerializeField] private TMP_Text titleEffectText;
    [SerializeField] private TMP_Text durationEffectText;
    
    [Header("Midle Part")]
    [SerializeField] private TMP_Text descriptionEffectText;
    
    [Header("Bottom Part")]
    [SerializeField] private TMP_Text effectsText;

    public void IntializeEffects(PerkSO perkData)
    {
        perkIcon.sprite = perkData.perkIcon;
        titleEffectText.text = perkData.perkName;
        durationEffectText.text = perkData.perkDuration.ToString();
        descriptionEffectText.text = perkData.perkDescription;

        effectsText.text = "";

        for (int i = 0; i < perkData.perkEffect.Length; i++)
        {
            PerkEffect effect = perkData.perkEffect[i];
            effectsText.text += $"{effect.effectName}\n";
        }
    }
}
