using UnityEngine;
using TMPro;

public class EnergyCurrencyUI : CurrencyUI
{
    private void OnEnable()
    {
        GlobalEvent.OnUpdateEnergyUI.Addistener(UpdateCurrencyUI);

    }

    private void OnDisable()
    {
        RemoveListener();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

    public override void UpdateCurrencyUI(int statusCurrency)
    {
        currencyValueText.text = $"{statusCurrency}";
    }

    public override void RemoveListener()
    {
        GlobalEvent.OnUpdateEnergyUI.Removeistener(UpdateCurrencyUI);
    }
}
