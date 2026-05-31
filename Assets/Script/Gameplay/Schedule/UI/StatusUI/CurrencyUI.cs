using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI currencyValueText;

    public virtual void UpdateCurrencyUI(int statusCurrency)
    {
    }


    public virtual void RemoveListener()
    {

    }

}
