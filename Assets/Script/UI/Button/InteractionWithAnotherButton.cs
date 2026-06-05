using TMPro;
using UnityEngine;

public class InteractionWithAnotherButton : MonoBehaviour
{
    public TextMeshProUGUI eventName;

    [Header("Energy UI Config")]
    public GameObject energyBoxArea;
    public TextMeshProUGUI enegryText;

    [Header("Money UI Config")]
    public GameObject moneyBoxArea;
    public TextMeshProUGUI moneyText;

    private EventOutDoor _eventData;
    
    public void InitializedButton(EventOutDoor eventData)
    {
        if (eventData == null) return;

        _eventData = eventData;
        eventName.text = eventData.eventName;

        if (eventData.energyCost > 0)
        {
            energyBoxArea.SetActive(true);
            enegryText.text = eventData.energyCost.ToString();
        }

        if (eventData.moneyCost > 0)
        {
            moneyBoxArea.SetActive(true);
            moneyText.text = eventData.moneyCost.ToString();
        }
    }
    
    public void OnInteractionButtonClick()
    {
        CurrencyManager.instance.UseCurrency(_eventData.energyCost, _eventData.moneyCost);

        foreach (var status in _eventData.statusAffected)
        {
            StatusManager.instance.CalculateStatusAfterRandomEffent(status.statusData.statusID, status.statusValue);
        }

        GlobalEvent.OnHidePanelInteractionAnother.Invoke();
    }
}
