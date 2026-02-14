using UnityEngine;
using System.Collections.Generic;

public class StatusManager : MonoBehaviour
{
    [Header("Status")]
    [Range(0, 100)]
    [SerializeField] private float ststusHappiness;
    [Range(0, 100)]
    [SerializeField] private float ststusSocial;
    [Range(0, 100)]
    [SerializeField] private float ststusFitness;
    [Range(0, 100)]
    [SerializeField] private float ststusIntelligence;
    [SerializeField] private int currency;

    [Header("Activity Status")]
    public List<activitySO> activitys;

    //TO CALCULATE EFFECTS FOR STATUS AND CURRENCY
    public void CallculaterEffects()
    {

    }
}
