using TMPro;
using UnityEngine;

public class InteractionWithAnotherButton : MonoBehaviour
{
    public TextMeshProUGUI eventName;

    public GameObject energyBoxArea;
    public TextMeshProUGUI enegryText;

    public GameObject moneyyBoxArea;
    public TextMeshProUGUI moneyText;

    public void InitializedButton(EventOutDoor eventData)
    {
        if (eventData == null) return;

        eventName.text = eventData.eventName;

        if (eventData.energyCost > 0)
        {
            energyBoxArea.SetActive(true);
            enegryText.text = eventData.energyCost.ToString();
        }

        if (eventData.moneyCost > 0)
        {
            moneyyBoxArea.SetActive(true);
            moneyText.text = eventData.moneyCost.ToString();
        }
    }
}
