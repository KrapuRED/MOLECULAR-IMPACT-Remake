using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    [Header("Currency Config")]
    [SerializeField] private StatusSO currecnyStatusData;
    [SerializeField] private float _statusCurrency;

    [Header("Events")]
    [SerializeField] private UpdateStatusCurrencyEventSO _updateStatusCurrencyUI;
    [SerializeField] private RefreshStatusUIEventSO _refreshStatusUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateCurrency(float currencyValue)
    {
        float amount = currencyValue;

        //calculate went have booster / perk increase money
        Debug.Log($"ModifierValue for Currency : {PerkManager.instance.GetModifierValue(currecnyStatusData.statusID)}");
        amount += amount * (PerkManager.instance.GetModifierValue(currecnyStatusData.statusID) / 100);

        _statusCurrency += amount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        _updateStatusCurrencyUI.OnRiase(_statusCurrency);
    }

    private void OnEnable()
    {
        _refreshStatusUI.Register(UpdateUI);
    }

    private void OnDisable()
    {
        _refreshStatusUI.Unregister(UpdateUI);
    }
}
