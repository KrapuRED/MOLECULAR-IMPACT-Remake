using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameState
{
    public List<ActivitySO> SelectedActivities = new List<ActivitySO>();
}
public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    [SerializeField] private GameState _gameState = new ();

    public List<ActivitySO> GetSelectedActivities() => _gameState.SelectedActivities;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetActivityGameState(List<ActivitySO> activityDatas)
    {
        if (activityDatas.Count <= 0)
        {
            Debug.LogWarning("Trying to add null activity to GameStateManager");
            return;
        }

        _gameState.SelectedActivities.Clear();

        _gameState.SelectedActivities = activityDatas;
    }
}
