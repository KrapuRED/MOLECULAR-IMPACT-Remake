using UnityEngine;

public class WorkDayManager : MonoBehaviour
{
    public static WorkDayManager instance;

    [SerializeField] private bool _isSideActivityDone;
    [SerializeField] private bool _isWorkDayDone;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        GlobalEvent.OnNextActivity.Addistener(ActiveWorkDay);
        GlobalEvent.OnResetManager.Addistener(ResetWorkDayManager);
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
        ResetWorkDayManager();
    }

    private void RemoveListeners()
    {
        GlobalEvent.OnNextActivity.Removeistener(ActiveWorkDay);
        GlobalEvent.OnResetManager.Removeistener(ResetWorkDayManager);
    }

    public void ActiveWorkDay()
    {
        GlobalEvent.OnUpdateVisualizeDay.Invoke(DayCycleManager.instance.CurrentDay);

        if (_isSideActivityDone && _isWorkDayDone)
        {
            GlobalEvent.OnNextDay.Invoke();
            return;
        }

        if (!_isWorkDayDone)
        {
            _isWorkDayDone = true;
            RandomDayEventManager.Instance.GetRandomDayEvent();
            return;
        }

        if (!_isSideActivityDone)
        {
            ActivityManager.instance.ActiveActivity();
            _isSideActivityDone = true;
            return;
        }
    }
    private void ResetWorkDayManager()
    {
        _isSideActivityDone = false;
        _isWorkDayDone = false;
    }
}
