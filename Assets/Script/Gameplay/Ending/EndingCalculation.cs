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

    private List<PlayerStatus> _status = new List<PlayerStatus>();


}
