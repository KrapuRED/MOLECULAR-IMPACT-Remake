using UnityEngine;

public enum StatusType
{
    Non_Currency,
    Currency
}

[CreateAssetMenu(fileName = "StatusSO", menuName = "Game Data/StatusSO")]
public class StatusSO : ScriptableObject
{
    public string statusID;
    public string statusName;
    public StatusType statusType;
    public int maxStatus;
    public Sprite statusIcon;
}
