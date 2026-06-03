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

    [SerializeField] private List<PlayerStatus> _status = new List<PlayerStatus>();


    [Header("Happiness")]
    [SerializeField] private float happinessHigh;
    [SerializeField] private float happinessLow;

    [Header("Status")]
    [SerializeField] private float statusHigh;
    [SerializeField] private float statusLow;


    private void Start()
    {
        //_status = StatusManager.instance.PlayerStatuses;
    }

    //public int CheckEnding()
    //{
    //    Debug.Log("Check Ending");
    //    if (GameManager.instance.financialRuin)
    //    {
    //        if (_status. < happinessHigh)
    //            return 1; //Depression Ending

    //        if (happiness >= happinessHigh && GameManager.instance.gamblingAddiction)
    //            return 2; //Beggar Ending
    //    }
    //    if (GameManager.instance.alcoholAddiction && fitness < fitnessLow)
    //    {
    //        return 3; //Short Lived Ending
    //    }
    //    if (GameManager.instance.gymRatsCircle || GameManager.instance.heavyExcercise || GameManager.instance.scholarCircle || GameManager.instance.dedicatedBookReading)
    //    {
    //        if (happiness >= happinessHigh)
    //        {
    //            if (GameManager.instance.heavyExcercise && GameManager.instance.gymRatsCircle && fitness >= statHigh)
    //            {
    //                return 4; //Athlete Life Ending
    //            }
    //            else if (GameManager.instance.dedicatedBookReading && GameManager.instance.scholarCircle && intelligence >= statHigh)
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
