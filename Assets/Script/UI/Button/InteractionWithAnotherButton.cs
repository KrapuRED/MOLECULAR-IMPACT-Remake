using TMPro;
using UnityEngine;

public class InteractionWithAnotherButton : MonoBehaviour
{
    public TextMeshProUGUI eventName;

    public GameObject energyBoxArea;
    public TextMeshProUGUI enegryText;

    public GameObject moneyyBoxArea;
    public TextMeshProUGUI moneyText;
    
    private int _energyCost;
    private int _moneyCost;
    
    public void InitializedButton(EventOutDoor eventData)
    {
        if (eventData == null) return;

        eventName.text = eventData.eventName;

        if (eventData.energyCost > 0)
        {
            energyBoxArea.SetActive(true);
            enegryText.text = eventData.energyCost.ToString();
            _energyCost =  eventData.energyCost;
        }

        if (eventData.moneyCost > 0)
        {
            moneyyBoxArea.SetActive(true);
            moneyText.text = eventData.moneyCost.ToString();
            _moneyCost = eventData.moneyCost;
        }
    }
    
    public void OnInteractionButtonClick()
    {
        CurrencyManager.instance.UseCurrency(_energyCost, _moneyCost);
        GlobalEvent.OnHidePanelInteractionAnother.Invoke();
    }
}
