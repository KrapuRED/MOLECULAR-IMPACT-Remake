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

    //        if (happinessVal >= happinessHigh && GameManager.instance.gamblingAddiction)
    //            return 2; //Beggar Ending
    //    }
    //    if (GameManager.instance.alcoholAddiction && fitnessVal < statusLow)
    //    {
    //        return 3; //Short Lived Ending
    //    }
    //    if (GameManager.instance.gymRatsCircle || GameManager.instance.heavyExcercise || GameManager.instance.scholarCircle || GameManager.instance.dedicatedBookReading)
    //    {
    //        if (happinessVal >= happinessHigh)
    //        {
    //            if (GameManager.instance.heavyExcercise && GameManager.instance.gymRatsCircle && fitnessVal >= statusHigh)
    //            {
    //                return 4; //Athlete Life Ending
    //            }
    //            else if (GameManager.instance.dedicatedBookReading && GameManager.instance.scholarCircle && intelligenceVal >= statusHigh)
    //            {
    //                return 5; //CEO Ending
    //            }

    //        }

    //    }

    //    if (GameManager.instance.badTagCount == 0)
    //    {
    //        return 6; //Happy Life Ending
    //    }
    //    //Nothing Interesting Ending Factor
    //    return 0;
    //}
}
