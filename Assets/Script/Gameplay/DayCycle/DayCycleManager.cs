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

    [Header("status week and day")]
    [SerializeField] private Days _startDay;
    [SerializeField] private Days _currentDay;
    [SerializeField] private WeekType _weekType;
    [SerializeField] private int _dayCount;
    [SerializeField] private int _weekCount;

    public WeekType WeekType => _weekType;
    public Days CurrenDay => _currentDay;
    public int WeekCount => _weekCount;

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

    private void OnEnable()
    {
        _refreshStatusUI.Register(UpdateUI);
        GlobalEvent.OnNextDayWorkDay.Addistener(NextDay);
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }
 
    private void Start()
    {
        InitializedDay();
    }

    private void InitializedDay()
    {
        _dayCount = 0;
        _weekCount = 0;

        CheckWeekType();

        _currentDay = _startDay;
        UpdateUI();
    }

    private void RemoveListeners()
    {
        GlobalEvent.OnNextDayWorkDay.Removeistener(NextDay);
        _refreshStatusUI.Unregister(UpdateUI);
    }

    public void NextDay()
    {
        CheckWeekType();

        _dayCount++;
        
        UpdateUI();

        CheckEnding(); // Buat Pindah ke Ending Scene
    }

    private void CheckWeekType()
    {
        int totalDay = (int)Days.Sunday + 1;
        int indexDay = _dayCount % totalDay ;
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
