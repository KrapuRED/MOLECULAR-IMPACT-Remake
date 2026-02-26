using System.Collections.Generic;
using UnityEngine;

#region Sub-Class Perk System
/* summary PerkStatus
 * Stores player perk progress (perk points).
 * This is the "status/progress" part of perk system.
 */
[System.Serializable]
public class PerkStatus
{
    public string perkName;
    public string perkID;
    public int perkPoint;
}

/* summary PerkData
 * Represents a perk definition + runtime state.
 * This references PerkSO which contains the perk configuration.
 */
[System.Serializable]
public class PerkData
{
    public PerkSO perkData;
    public bool isActive;
}

/* summary PerkDataTemporary
 * Represents an instance of a temporary perk.
 * Temporary perks must store activation time + expiration time,
 * because multiple temporary perks can expire at different weeks.
 */
[System.Serializable]
public class PerkDataTemporary
{
    public PerkSO perkData;
    public int activatedWeek;
    public int expireWeek;
    public bool isActive;
}
#endregion


/* summary PerkManager
 * PerkManager is the main controller of the perk system.
 * Responsibilities:
 *  1. Initialize perk status list and dictionary.
 *  2. Update perk points when activity rewards are received.
 *  3. Check perk requirements and activate perks.
 *  4. Manage permanent and temporary perk lists.
 *  5. Check expiration of temporary perks using DayCycleManager.
 */
public class PerkManager : MonoBehaviour
{
    public static PerkManager instance;

    [Header("Status Perk")]
    // Dictionary used for fast lookup:
    // perkID -> PerkStatus (player progress)
    private Dictionary<string, PerkStatus> _perkStatusDictionary = new Dictionary<string, PerkStatus>();
    // This list is visible in inspector for debugging.
    [SerializeField] private List<PerkStatus> _perkStatus = new List<PerkStatus>();

    //Perk Data Management
    [Header("Config Perk Data")]
    //All data perk
    [SerializeField] private PerkData[] perkDatas;
    //save permanet perk
    [SerializeField] private List<PerkData> _activePerkPermanentDatas = new List<PerkData>();
    //save commitment perk
    [SerializeField] private List<PerkData> _activePerkCommitmentDatas = new List<PerkData>();
    //save tempory perk
    [SerializeField] private List<PerkDataTemporary> _activePerkTemporaryDatas =  new List<PerkDataTemporary>();

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        InitiliazePerk();
    }

    /* Summary InitializePerk
     * 1. Create _perkStatus entries based on perkDatas array.
     * 2. Store _perkStatus into dictionary for fast access by perkID.
     */
    private void InitiliazePerk()
    {
        //Clear existing data
        _perkStatusDictionary.Clear();
        _perkStatus.Clear();

        if (perkDatas == null || perkDatas.Length == 0)
        {
            Debug.LogWarning("Perk Data array is empty or null. No perks will be initialized.");
            return;
        }

        foreach (var perk in perkDatas)
        {
            if  (perk == null || perk.perkData == null)
            {
                Debug.LogWarning("There are NULL in perk Data");
                continue;
            }

            var newPerkStatus = new PerkStatus
            {
                perkName = perk.perkData.perkName,
                perkID = perk.perkData.perkID,
                perkPoint = 0
            };
            
            _perkStatus.Add(newPerkStatus);
        }

        //assign PerkStatus to dictionary
        foreach (var perk in _perkStatus)
        {
            if (!_perkStatusDictionary.ContainsKey(perk.perkID))
            {
                _perkStatusDictionary.Add(perk.perkID, perk);
            }
        }

    }

    #region Perk System
    /* Summary UpdatePerk
     * Updates perk points based on ActivitySO rewards.
     * This should be called whenever player completes an activity.
     */
    public void UpdatePerk(ActivitySO activityData)
    {
        if (activityData == null)
        {
            Debug.LogError("activityData is Null");
            return;
        }
        if (activityData.perkStatus == null)
        {
            Debug.LogError("activityData is not having PerkStatus");
            return;
        }

        //Find the perkID in dictionary
        for (int i = 0; i < activityData.perkStatus.Length; i++)
        {
            if (_perkStatusDictionary.ContainsKey(activityData.perkStatus[i].perkID))
            {
                //add the perk point
                _perkStatusDictionary[activityData.perkStatus[i].perkID].perkPoint += activityData.perkStatus[i].perkPoint;

                //Check if some perk is pass the requirement
                var perk = _perkStatusDictionary[activityData.perkStatus[i].perkID];
                CheckPerkRequirement(perk.perkID, perk.perkPoint);
            }
            else
            {
                Debug.LogError($"There no {activityData.perkStatus[i].perkID} inside of Perk Status Dictionary!");
            }
        }
    }

    /* Summary CheckPerkRequirement
     * Checks if a perk meets requirement and activates it.
     * If activated, the perk is stored into correct category list
     * (Permanent or Temporary).
     */
    private void CheckPerkRequirement(string PerkID, int perkPoint)
    {
        Debug.Log($"Check Requirement of {PerkID} with {perkPoint} points");

        if (!_perkStatusDictionary.TryGetValue(PerkID, out var pd) || pd == null)
            return;

        for (int i = 0; i < perkDatas.Length; i++)
        {
            //Debug.Log($"{perkDatas[i].perkDataSO.PerkID} : {perkDatas[i].perkDataSO.perkRequirement}");
            if (perkDatas[i].perkData != null && perkDatas[i].perkData.perkID == PerkID && perkPoint >= perkDatas[i].perkData.perkRequirement && !perkDatas[i].isActive)
            {
                perkDatas[i].isActive = true;

                //save perkdata to each correct data
                switch (perkDatas[i].perkData.perkDuration)
                {
                    case PerkDuration.Permanent:
                        //add to permanent perk
                        SetPerkDataPermanent(perkDatas[i]);
                        break;
                    case PerkDuration.Temporary:
                        //add to temporary perk and set for end duration
                        SetPerkDataTemporary(perkDatas[i]);
                        break;
                    default:
                        break;
                }
                Debug.Log($"{perkDatas[i].perkData.perkName} is been active");
                break;
            }
        }
    }

    /* Summary CheckDurationTemporaryPerk
     * Checks all temporary perks and removes expired ones.
     * This should be called whenever week changes (or daily tick).
     */
    public void CheckDurationTemporaryPerk()
    {
        //Check indivudual Duration and indivudual temporary have diffrent start and end
        //Debug.Log("total _activePerkTemporaryDatas : " + _activePerkTemporaryDatas.Count);
        for (int i = _activePerkTemporaryDatas.Count - 1; i >= 0; i--)
        {
            if (_activePerkTemporaryDatas[i] == null)
                continue;

            if (DayCycleManager.instance.weekCount >= _activePerkTemporaryDatas[i].expireWeek)
            {
                //deactive in PerkData
                DeactivedTemporaryPerk(_activePerkTemporaryDatas[i].perkData);
                //Tell HUD / UI that some perk are expire 

                //Remove from temporaryPerkData
                _activePerkTemporaryDatas.RemoveAt(i);
            }
        }
    }

    /* Summary DeactivedTemporaryPerk
     * Deactivates a temporary perk inside perkDatas[].
     * This ensures perk active state is consistent.
     */
    private void DeactivedTemporaryPerk(PerkSO perkDataSO)
    {
        //find perk Data using the perkID
        Debug.Log($"Deactive {perkDataSO.perkName}");
        //Need to be revears
        for (int i = 0; i < perkDatas.Length; i++)
        {
            if (perkDataSO.perkID == perkDatas[i].perkData.perkID && perkDatas[i].isActive)
            {
                Debug.Log($"Succes Deactive {perkDataSO.perkName}");
                //UpdatePerkModify();
                perkDatas[i].isActive = false;
            }
        }
    }

    /* Summary SetPerkDataTemporary
     * Creates and stores a new temporary perk instance.
     * This stores the activation week and calculated expiration week.
     */
    private void SetPerkDataTemporary(PerkData perkDataSO)
    {
        PerkDataTemporary newPerkDataTemporary = new PerkDataTemporary();

        newPerkDataTemporary.perkData = perkDataSO.perkData;
        newPerkDataTemporary.activatedWeek = DayCycleManager.instance.weekCount;
        newPerkDataTemporary.expireWeek = perkDataSO.perkData.longDuration + DayCycleManager.instance.weekCount;
        newPerkDataTemporary.isActive = perkDataSO.isActive;

        UpdatePerkModify(perkDataSO);
        _activePerkTemporaryDatas.Add(newPerkDataTemporary);
    }

    private void SetPerkDataPermanent(PerkData perkDataSO)
    {
        PerkData newPermanetPerk = new PerkData();

        newPermanetPerk.perkData = perkDataSO.perkData;
        newPermanetPerk.isActive = perkDataSO.isActive;

        UpdatePerkModify(perkDataSO);
        _activePerkPermanentDatas.Add(newPermanetPerk);
    }

    private void UpdatePerkModify(PerkData perkDataSO)
    {
        //went get active or deactive perk modify need to change

    }
    #endregion

    public float GetModifierValue(string statusID)
    {
        float totalModify = 0;

        //Check Tempory Perk
        foreach (var TemporyPerk in _activePerkTemporaryDatas)
        {
            if (!TemporyPerk.isActive) continue;

            foreach (var effect in TemporyPerk.perkData.perkEffect)
            {
                if (effect.statusData.statusID == statusID)
                {
                    totalModify += effect.effectValue;
                }
            }
        }

        //Check Permanent Perk
        foreach (var permanetPerk in _activePerkPermanentDatas)
        {
            if (!permanetPerk.isActive) continue;

            foreach (var effect in permanetPerk.perkData.perkEffect)
            {
                if (effect.statusData.statusID == statusID)
                {
                    totalModify += effect.effectValue;
                }
            }
        }

        return totalModify;
    }

    public void UpdeteUI()
    {
        List<PerkSO> pd = new List<PerkSO>();

        foreach (var perk in _activePerkTemporaryDatas)
        {
            if (perk.perkData == null || !perk.isActive)
            {
                continue;
            }
            pd.Add(perk.perkData);
        }

        foreach(var perk in _activePerkPermanentDatas)
        {
            if (perk.perkData == null || !perk.isActive)
            {
                continue;
            }
            pd.Add(perk.perkData);
        }

        TestDictionary(pd);
    }

    private void TestDictionary(List<PerkSO> perkData)
    {
        foreach (var perk in perkData)
            Debug.Log($"{perk.perkName}");
    }
}
