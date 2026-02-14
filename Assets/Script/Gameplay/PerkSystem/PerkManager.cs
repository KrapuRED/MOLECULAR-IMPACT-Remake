using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PerkStatus
{
    public string perkName;
    public int perkPoint;
}

[System.Serializable]
public class PerkData
{
    public PerkSO perkData;
    public bool isActive;
    [SerializeField] private int duration;
}
/*
 * Make public class that store PerkSO
 * Then make that class for active / deactive perk (for tempory)
  */

public class PerkManager : MonoBehaviour
{
    public static PerkManager instance;

    [Header("Status Perk")]
    private Dictionary<string, PerkStatus> _perkStatusDictionary = new Dictionary<string, PerkStatus>();
    public PerkStatus[] perkStatus;

    //Perk Data Management
    [Header("Config Perk Data")]
    //All data perk
    [SerializeField] private PerkData[] perkDatas;
    //save permanet perk
    [SerializeField] private PerkData[] perkPermanentDatas;
    //save commitment oerk
    [SerializeField] private PerkData[] perkCommitmentDatas;
    //save tempory perk
    [SerializeField] private PerkData[] perkTemporyDatas;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        //assign to dictionary
        foreach (var perk in perkStatus)
        {
            if (!_perkStatusDictionary.ContainsKey(perk.perkName))
            {
                _perkStatusDictionary.Add(perk.perkName, perk);
            }
        }
    }

    public void UpdatePerk(ActivitySO activityData)
    {
        //Find the perkName in dictionary
        for (int i = 0; i < activityData.perkStatus.Length; i++)
        {
            if (_perkStatusDictionary.ContainsKey(activityData.perkStatus[i].perkName))
            {
                //add the perk point
                _perkStatusDictionary[activityData.perkStatus[i].perkName].perkPoint += activityData.perkStatus[i].perkPoint;

                //Check if some perk is pass the requirement
                var perk = _perkStatusDictionary[activityData.perkStatus[i].perkName];
                CheckPerkRequirement(perk.perkName, perk.perkPoint);
            }
            else
            {
                Debug.LogError($"There no {activityData.perkStatus[i].perkName} inside of Perk Status Dictionary!");
            }
        }
    }

    private void CheckPerkRequirement(string perkName, int perkPoint)
    {
        Debug.Log($"Check Requirement of {perkName} with {perkPoint}");
        for (int i = 0; i < perkDatas.Length; i++)
        {
            Debug.Log($"{perkDatas[i].perkData.perkName} : {perkDatas[i].perkData.perkRequirement}");
            if (perkDatas[i].perkData.perkName == perkName && perkPoint >= perkDatas[i].perkData.perkRequirement && !perkDatas[i].isActive)
            {
                perkDatas[i].isActive = true;
                Debug.Log($"{perkDatas[i].perkData.perkName} is been active");
            }
        }
    }

    private void CheckDurationTemporyPerk()
    {

    }

    private void TestDictionary()
    {
        foreach (var perk in _perkStatusDictionary)
            Debug.Log($"{perk.Key} : {perk.Value.perkPoint}");
    }
}
