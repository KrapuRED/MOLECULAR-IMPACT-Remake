using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyValueText;
    public UpdateStatusCurrencyEventSO updateStatusCurrency;

    public void UpdateCurrencyUI(int statusCurrency)
    {
        currencyValueText.text = $"{statusCurrency}";
    }

    private void OnEnable()
    {
        updateStatusCurrency.Register(UpdateCurrencyUI);
    }

    private void OnDisable()
    {
        updateStatusCurrency.Unregister(UpdateCurrencyUI);
    }
}
