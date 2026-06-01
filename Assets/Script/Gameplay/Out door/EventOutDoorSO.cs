using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EventOutDoor
{
    public string eventName;
    public string eventID;
    public int energyCost;
    public int moneyCost;

    // Add or Gain more for the status
}


[CreateAssetMenu(fileName = "EventOutDoorSO", menuName = "Event Out Door/EventOutDoorSO")]
public class EventOutDoorSO : ScriptableObject
{
    public List<EventOutDoor> eventOutDoorList = new();
}
