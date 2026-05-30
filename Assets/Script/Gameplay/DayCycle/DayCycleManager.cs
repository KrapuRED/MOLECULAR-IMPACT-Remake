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
    Sunday
}

public enum WeekType
{
    weekDay,
    weekEnd
}

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager instance;

    [Header("Settings")]
    [SerializeField] private Days _startDay = Days.Sunday;

    [Header("Runtime State - ReadOnly")]
    [SerializeField] private Days _currentDay;
    [SerializeField] private WeekType _weekType;
    [SerializeField] private int _dayCount;   // 0 = first day of game, counts up forever
    [SerializeField] private int _weekCount;

    public Days CurrentDay => _currentDay;
    public WeekType WeekType => _weekType;
    public int DayCount => _dayCount;
    public int WeekCount => _weekCount;

    [Header("Events")]
    public UpdateWeekCountEventSO updateWeekCountEvent;
    [SerializeField] private RefreshStatusUIEventSO _refreshStatusUI;

    private bool _isProcessingDay = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return; //  return immediately, don't run anything below
        }

        instance = this;
        InitializedDay();

        _refreshStatusUI.Register(UpdateUI);
        GlobalEvent.OnNextDay.Addistener(NextDay);
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void InitializedDay()
    {
        _dayCount = 0;
        _weekCount = 0;
        _isProcessingDay = false;
        _currentDay = _startDay;
        _weekType = GetWeekType(_startDay);

        UpdateUI();
        Debug.Log($"[DayCycleManager] Initialized -> Day: {_currentDay} | Week: {_weekCount}");
    }

    private void RemoveListeners()
    {
        GlobalEvent.OnNextDay.Removeistener(NextDay);
        _refreshStatusUI.Unregister(UpdateUI);
    }

    public void NextDay()
    {
        if (this == null) return;

        if (_isProcessingDay)
        {
            Debug.LogWarning("[DayCycleManager] NextDay blocked — already processing");
            return;
        }

        _isProcessingDay = true;
        _dayCount++;

        // Calculate current day from start offset
        int totalDays = System.Enum.GetValues(typeof(Days)).Length; // 7
        int startOffset = (int)_startDay;
        int indexDay = (_dayCount + startOffset) % totalDays;

        _currentDay = (Days)indexDay;
        _weekType = GetWeekType(_currentDay);

        // New week started
        if (indexDay == (int)_startDay && _dayCount > 0)
        {
            _weekCount++;
            OnNewWeek();
        }

        Debug.Log($"[DayCycleManager] Day {_dayCount} -> {_currentDay} | Week {_weekCount} | {_weekType}");

        UpdateUI();

        HandleSceneTransition();

        CheckEnding();

        _isProcessingDay = false;
        GlobalEvent.OnResetManager.Invoke();
    }

    private WeekType GetWeekType(Days day)
    {
        return (day == Days.Saturday || day == Days.Sunday)
            ? WeekType.weekEnd
            : WeekType.weekDay;
    }

    private void OnNewWeek()
    {
        PerkManager.instance.CheckDurationTemporaryPerk();
        Debug.Log($"[DayCycleManager] New week started -> Week {_weekCount}");
    }

    private void HandleSceneTransition()
    {
        if (_dayCount == 0) return;

        switch (_currentDay)
        {
            case Days.Saturday:
                Debug.Log("[DayCycleManager] Weekend! Touch some grass.");
                break;
            case Days.Sunday:
                SceneController.instance.GameplayScene();
                break;
            default:
                SceneController.instance.WorkDayScene();
                break;
        }
    }

    private void CheckWeekType()
    {
        int totalDay = (int)Days.Sunday + 1;
        int indexDay = _dayCount % totalDay;
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

    public void UpdateUI()
    {
        updateWeekCountEvent.RaiseEvent(_weekCount);
    }

    private void CheckEnding()
    {
        if(_weekCount <= 10) return;

        Debug.Log("Ending Time");
        SceneController.instance.EndingScene();
    }
}
