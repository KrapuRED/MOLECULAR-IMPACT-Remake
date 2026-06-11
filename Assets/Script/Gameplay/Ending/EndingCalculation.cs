using System.Collections.Generic;
using UnityEngine;

public class EndingCalculation : MonoBehaviour
{
    public static EndingCalculation instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private List<StatusData> _status = new List<StatusData>();


    [Header("All Status Value")]
    [SerializeField] private float happinessVal = 0;
    [SerializeField] private float socialVal = 0;
    [SerializeField] private float fitnessVal = 0;
    [SerializeField] private float intelligenceVal = 0;
    [SerializeField] private float currencyVal = 0;


    [Header("Happiness")]
    [SerializeField] private float happinessHigh;
    [SerializeField] private float happinessLow;

    [Header("Status")]
    [SerializeField] private float statusHigh;
    [SerializeField] private float statusLow;

    [Header("All Perk ID")]
    [SerializeField] private string gamblingAddictionID;
    [SerializeField] private string alcoholAddictionID;
    [SerializeField] private string gymRatsCircleID;
    [SerializeField] private string heavyExcerciseID;
    [SerializeField] private string scholarCircleID;
    [SerializeField] private string dedicatedBookReadingID;

    private void Start()
    {
        if(StatusManager.instance != null)
            _status = StatusManager.instance.PlayerStatuses;
        ReadAllStatusFromList();
    }

    public void ReadAllStatusFromList()
    {
        foreach (StatusData status in _status)
        {
            // Pastikan statusData tidak null agar tidak error NullReferenceException
            if (status.statusData != null)
            {
                // Cek berdasarkan statusID atau statusName yang kamu atur di Inspector
                if (status.statusData.statusID == "status_happiness")
                {
                    happinessVal = status.statusValue;
                }
                else if (status.statusData.statusID == "status_social")
                {
                    socialVal = status.statusValue;
                }
                else if (status.statusData.statusID == "status_fitness")
                {
                    fitnessVal = status.statusValue;
                }
                else if (status.statusData.statusID == "status_intelligence")
                {
                    intelligenceVal = status.statusValue;
                }
                // Kamu juga bisa cek berdasarkan enum statusType jika tipenya Currency
                else if (status.statusData.statusType == StatusType.Currency)
                {
                    currencyVal = status.statusValue;
                }
            }
        }
    }


    //public int CheckEnding()
    //{
    //    Debug.Log("Check Ending");
    //    if (currencyVal < 0)
    //    {
    //        if (happinessVal < happinessHigh)
    //            return 1; //Depression Ending

    //        if (happinessVal >= happinessHigh && PerkManager.instance.IsPerkActive(gamblingAddictionID))
    //            return 2; //Beggar Ending
    //    }
    //    if (PerkManager.instance.IsPerkActive(alcoholAddictionID) && fitnessVal < statusLow)
    //    {
    //        return 3; //Short Lived Ending
    //    }
    //    if (PerkManager.instance.IsPerkActive(gymRatsCircleID) || PerkManager.instance.IsPerkActive(heavyExcerciseID) || PerkManager.instance.IsPerkActive(scholarCircleID) || PerkManager.instance.IsPerkActive(dedicatedBookReadingID))
    //    {
    //        if (happinessVal >= happinessHigh)
    //        {
    //            if (PerkManager.instance.IsPerkActive(heavyExcerciseID) && PerkManager.instance.IsPerkActive(gymRatsCircleID) && fitnessVal >= statusHigh)
    //            {
    //                return 4; //Athlete Life Ending
    //            }
    //            else if (PerkManager.instance.IsPerkActive(dedicatedBookReadingID) && PerkManager.instance.IsPerkActive(scholarCircleID) && intelligenceVal >= statusHigh)
    //            {
    //                return 5; //CEO Ending
    //            }

    //        }

    //    }

    //    if (!PerkManager.instance.IsThereAnyBadTag())
    //    {
    //        return 6; //Happy Life Ending
    //    }
    //    //Nothing Interesting Ending Factor
    //    return 0;
    //}

    public int CheckEnding()
    {
        Debug.Log("Check Ending");
        if (currencyVal < 0)
        {
            if (happinessVal < happinessHigh)
                return 1; //Depression Ending

            if (happinessVal >= happinessHigh)
                return 2; //Beggar Ending
        }
        if (PerkManager.instance.IsPerkActive(alcoholAddictionID) && fitnessVal < statusLow)
        {
            return 3; //Short Lived Ending
        }
        //if (PerkManager.instance.IsPerkActive(gymRatsCircleID) || PerkManager.instance.IsPerkActive(heavyExcerciseID) || PerkManager.instance.IsPerkActive(scholarCircleID) || PerkManager.instance.IsPerkActive(dedicatedBookReadingID))
        //{
        if (happinessVal >= happinessHigh)
        {
            if (fitnessVal >= statusHigh)
            {
                return 4; //Athlete Life Ending
            }
            else if (intelligenceVal >= statusHigh)
            {
                return 5; //CEO Ending
            }

        }

        //}

        if (!PerkManager.instance.IsThereAnyBadTag())
        {
            return 6; //Happy Life Ending
        }
        //Nothing Interesting Ending Factor
        return 0;
    }
}
