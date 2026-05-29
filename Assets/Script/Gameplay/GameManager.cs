using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private DayCycleManager _dayCycleManager;
    private StatusManager _statusManager;
    private PerkManager _perkManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _dayCycleManager = DayCycleManager.instance;
        _statusManager = StatusManager.instance;
        _perkManager = PerkManager.instance;

    }

    public void NextGamePlay()
    {
        SceneController.instance.WorkDayScene();
    }
}
