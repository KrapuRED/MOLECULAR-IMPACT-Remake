using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    [Header("Currency Config")]
    [SerializeField] private StatusSO currecnyStatusData;
    [SerializeField] private float startingMoneyCurrency;
    [SerializeField] private int moneyEarnedPerWeek = 300;
    [Range(0, 100)]
    [SerializeField] private float moneyBoosterPercentage;
    [SerializeField] private float _currentMoneyCurrency;

    [Header("Status Energy")]
    [SerializeField] private int _maxEnergy = 100;
    [SerializeField] private int _currentEnergy;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        _currentMoneyCurrency = startingMoneyCurrency;
        _currentEnergy = _maxEnergy;
    }

    public void UpdateUI()
    {
        GlobalEvent.OnUpdateMoneyUI.Invoke((int)_currentMoneyCurrency);
        GlobalEvent.OnUpdateEnergyUI.Invoke(_currentEnergy);
    }

    public void RefershCurrency()
    {
        Debug.Log("RefershCurrency Get called");

        RefershMoney();

        RefershEnergy();
    }

    public void UseCurrency(int energyCost, int moneyCost)
    {
        ConsumeMoney(moneyCost);
        ConsumeEnergy(energyCost);
    }

    // ======================= MONEY SYSTEM =======================
    public void RefershMoney()
    {
        //float amount = moneyEarnedPerWeek * moneyBoosterPercentage;
        Debug.Log("RefershMoney Get called");
        _currentMoneyCurrency += moneyEarnedPerWeek;

        UpdateUI();
    }

    public void ConsumeMoney(int amount)
    {
        _currentMoneyCurrency -= amount;
        
        if (_currentMoneyCurrency < 0)
            _currentMoneyCurrency = 0;

        // Update energy UI here if needed
        UpdateUI();
    }

    public void UpdateMoneyBoosterCurrency(float currencyValue)
    {
        float amount = currencyValue;

        //calculate went have booster / perk increase money
        Debug.Log($"ModifierValue for Currency : {PerkManager.instance.GetModifierValue(currecnyStatusData.statusID)}");
        amount += amount * (PerkManager.instance.GetModifierValue(currecnyStatusData.statusID) / 100);

        _currentMoneyCurrency += amount;
        UpdateUI();
    }

    // ======================= ENERGY SYSTEM =======================
    public void RefershEnergy()
    {
        _currentEnergy = _maxEnergy;
    }

    public void ConsumeEnergy(int amount)
    {
        _currentEnergy -= amount;
        if (_currentEnergy < 0)
            _currentEnergy = 0;

        // Update energy UI here if needed
        UpdateUI();
    }
}
