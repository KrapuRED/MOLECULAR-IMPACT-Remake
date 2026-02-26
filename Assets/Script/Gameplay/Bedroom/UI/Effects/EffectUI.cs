using TMPro;
using UnityEngine;

public class EffectUI : MonoBehaviour
{
    [SerializeField] private PerkSO _perkData;
    public TextMeshProUGUI tagNameText;
    public TextMeshProUGUI tagDurationText;
    public TextMeshProUGUI tagDescriptionText;
    public TextMeshProUGUI[] effects;

    private void Start()
    {
        UpdateEffectUI(_perkData);
    }

    private void UpdateEffectUI(PerkSO perkData)
    {
        if (_perkData == null)
            _perkData = perkData;

        tagNameText.text = _perkData.perkName;
        tagDurationText.text = _perkData.longDuration.ToString() + "s";
        tagDescriptionText.text = _perkData.perkDescription;

        for (int i = 0; i < _perkData.perkEffect.Length; i++)
        {
            effects[i].text = _perkData.perkEffect[i].effectName;
        }
    }
}
