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
    public Days currenData => _currentDay;
    [SerializeField] private WeekType _weekType;
    public WeekType weekType => _weekType;
    [SerializeField] private int _dayCount;
    [SerializeField] private int _weekCount;

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

        //get the _startDay by day count
        _currentDay = _startDay = (Days)_dayCount;

    }

    private void NextDay()
    {
        _dayCount++;
        int indexDay = _dayCount % (int)Days.COUNT;
        _currentDay = (Days)indexDay;

        //To Change WeekType
        if (_currentDay != Days.Saturday)
            _weekType = WeekType.weekDay;
        else
            _weekType = WeekType.weekEnd;

        //count week
        if (indexDay == 0)
            _weekCount++;
    }

    private IEnumerator TestDayCycle()
    {
        yield return new WaitForSeconds(2.5f);
        NextDay();
    }
}
