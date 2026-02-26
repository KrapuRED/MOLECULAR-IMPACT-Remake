using System.Collections;
using UnityEngine;

public enum Days
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday ,
    Friday ,
    Saturday,
    Sunday,
    COUNT
}

public enum WeekType
{
    weekDay,
    weekEnd
}

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager instance;

    [Header("status week and day")]
    [SerializeField] private Days _startDay;
    [SerializeField] private Days _currentDay;
    [SerializeField] private WeekType _weekType;
    [SerializeField] private int _dayCount;
    [SerializeField] private int _weekCount;

    public WeekType weekType => _weekType;
    public Days currenData => _currentDay;
    public int weekCount => _weekCount;

    [Header("Events")]
    public UpdateWeekCountEventSO updateWeekCountEvent;
    [SerializeField] private RefreshStatusUIEventSO _refreshStatusUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _dayCount = 0;
        _weekCount = 0;

        CheckWeekType();

        //get the _currentDay by _startDay
        _currentDay = _startDay;
        UpdateUI();
        //StartCoroutine(TestDayCycle());
    }

    public void NextDay()
    {
        _dayCount++;
        
        CheckWeekType();

        UpdateUI();
        //StartCoroutine(TestDayCycle());
    }

    private void CheckWeekType()
    {
        int indexDay = _dayCount % (int)Days.COUNT;
        _currentDay = (Days)indexDay;

        //To Change WeekType
        if (indexDay <= 4)
            _weekType = WeekType.weekDay;
        else
            _weekType = WeekType.weekEnd;

        //count week
        if (indexDay == 0)
        {
            _weekCount++;
            PerkManager.instance.CheckDurationTemporaryPerk();
        }
    }

    private IEnumerator TestDayCycle()
    {
        yield return new WaitForSeconds(2.5f);
        NextDay();
    }

    public void UpdateUI()
    {
        updateWeekCountEvent.RaiseEvent(_weekCount);
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
