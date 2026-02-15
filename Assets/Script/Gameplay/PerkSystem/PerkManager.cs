using System.Collections.Generic;
using UnityEngine;

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

/* READ THIS BEFORE GO NEXT
 * Sugget BY ChatGPT
 * BUGS :
 *  BUG 2: You never break; after activating
 *  BUG 3: Duplicate entries in _perkPermanentDatas
 *  BUG 4: Dictionary key = PerkID string (dangerous long-term)
 *  BUG 5: No null checks in perkDatas loop
 *  BUG 6: Temporary perk logic is incomplete
 *  BUG 7: duration is private and unused
 * 
 * Issues
 *  ISSUE 1: perkStatus[] and perkDatas[] are disconnected
 *  ISSUE 2: perkDatas should be dictionary too
 */

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
    [SerializeField] private List<PerkStatus> perkStatus = new List<PerkStatus>();

    //Perk Data Management
    [Header("Config Perk Data")]
    //All data perk
    [SerializeField] private PerkData[] perkDatas;
    //save permanet perk
    [SerializeField] private List<PerkData> _perkPermanentDatas = new List<PerkData>();
    //save commitment oerk
    [SerializeField] private List<PerkData> _perkCommitmentDatas = new List<PerkData>();
    //save tempory perk
    [SerializeField] private List<PerkDataTemporary> _perkTemporaryDatas =  new List<PerkDataTemporary>();


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
     * 1. Create perkStatus entries based on perkDatas array.
     * 2. Store perkStatus into dictionary for fast access by perkID.
     */
    private void InitiliazePerk()
    {
        // assign PerkStatus from perkDataSO
        foreach (var perk in perkDatas)
        {
            if  (perk.perkData == null)
            {
                Debug.LogError("There are NULL in perk Data");
                return;
            }

            PerkStatus newPerkStatus = new PerkStatus();
            newPerkStatus.perkName = perk.perkData.perkName + " Status";
            newPerkStatus.perkID = perk.perkData.perkID;
            newPerkStatus.perkPoint = 0;
            perkStatus.Add(newPerkStatus);
        }

        //assign PerkStatus to dictionary
        foreach (var perk in perkStatus)
        {
            if (!_perkStatusDictionary.ContainsKey(perk.perkID))
            {
                _perkStatusDictionary.Add(perk.perkID, perk);
            }
        }
    }

    /* Summary UpdatePerk
     * Updates perk points based on ActivitySO rewards.
     * This should be called whenever player completes an activity.
     */
    public void UpdatePerk(ActivitySO activityData)
    {
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
        TestDictionary();
    }

    /* Summary CheckPerkRequirement
     * Checks if a perk meets requirement and activates it.
     * If activated, the perk is stored into correct category list
     * (Permanent or Temporary).
     */
    private void CheckPerkRequirement(string PerkID, int perkPoint)
    {
        Debug.Log($"Check Requirement of {PerkID} with {perkPoint}");
        for (int i = 0; i < perkDatas.Length; i++)
        {
            //Debug.Log($"{perkDatas[i].perkDataSO.PerkID} : {perkDatas[i].perkDataSO.perkRequirement}");
            if (perkDatas[i].perkData.perkID == PerkID && perkPoint >= perkDatas[i].perkData.perkRequirement && !perkDatas[i].isActive)
            {
                perkDatas[i].isActive = true;

                //save perkdata to each correct data
                switch (perkDatas[i].perkData.perkDuration)
                {
                    case PerkDuration.Permanent:
                        //add to permanent perk
                        PerkData newPermanetPerk = new PerkData();
                        newPermanetPerk.perkData = perkDatas[i].perkData;
                        newPermanetPerk.isActive = perkDatas[i].isActive;
                        _perkPermanentDatas.Add(newPermanetPerk);
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
        Debug.Log($"Checking perk on week {DayCycleManager.instance.weekCount}");
        //Check indivudual Duration and indivudual temporary have diffrent start and end
        Debug.Log("total _perkTemporaryDatas : " + _perkTemporaryDatas.Count);
        for (int i = _perkTemporaryDatas.Count - 1; i >= 0; i--)
        {
            if (_perkTemporaryDatas[i] == null)
                continue;

            if (DayCycleManager.instance.weekCount >= _perkTemporaryDatas[i].expireWeek)
            {
                //deactive in PerkData
                DeactivedTemporaryPerk(_perkTemporaryDatas[i].perkData);
                //Tell HUD / UI that some perk are expire 
                //Remove from temporaryPerkData
                _perkTemporaryDatas.RemoveAt(i);
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
        for (int i = 0; i < perkDatas.Length; i++)
        {
            if (perkDataSO.perkID == perkDatas[i].perkData.perkID && perkDatas[i].isActive)
            {
                Debug.Log($"Succes Deactive {perkDataSO.perkName}");
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
        _perkTemporaryDatas.Add(newPerkDataTemporary);
    }

    private void TestDictionary()
    {
        foreach (var perk in _perkStatusDictionary)
            Debug.Log($"{perk.Key} : {perk.Value.perkPoint}");
    }
}
