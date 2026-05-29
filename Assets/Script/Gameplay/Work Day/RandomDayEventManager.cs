using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class RandomDayEvent
{
    public string EventName;
    public StatusSO EventStatusEffected;
    public Sprite illustrastionImg;
    public float EffectValue;
    [Range(0, 100)]
    public int percentageChance;
}
public class RandomDayEventManager : MonoBehaviour
{
    public static RandomDayEventManager Instance { get; private set; }

    public List<RandomDayEvent> randomDayEvents = new List<RandomDayEvent>();

    private bool isEventTriggered = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        GlobalEvent.OnResetManager.Removeistener(ResetEventTrigger);
        GlobalEvent.OnNextDay.Removeistener(GetRandomDayEvent);

    }

    public void GetRandomDayEvent()
    {
        if (this == null) return; // Check if the instance is still valid

        if (isEventTriggered) return; // Prevent multiple events in the same day

        int roll = Random.Range(0, 100);
        int cumulativeChance = 0;

        foreach (var randomEvent in randomDayEvents)
        {
            cumulativeChance += randomEvent.percentageChance;
            if (roll < cumulativeChance)
            {
                if (isEventTriggered) return;// Double-check to prevent multiple triggers in case of overlapping chances

                //Debug.Log($"Random Event Triggered: {randomEvent.EventName} affecting {randomEvent.EventStatusEffected.name} by {randomEvent.EffectValue}");
                GlobalEvent.OnShowIllustrastionWorkDay.Invoke(randomEvent.illustrastionImg);
                GlobalEvent.OnApplyRandomDayEvent.Invoke(randomEvent.EventStatusEffected.statusID, randomEvent.EffectValue);

                isEventTriggered = true;
            }
        }
    }

    public void ResetEventTrigger()
    {
        if (this == null) return; // Check if the instance is still valid

        //Debug.Log("Resetting Random Day Event Trigger for the new day.");
        isEventTriggered = false;
    }
}
