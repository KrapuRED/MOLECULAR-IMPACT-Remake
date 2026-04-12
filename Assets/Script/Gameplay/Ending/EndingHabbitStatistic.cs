using UnityEngine;

public class EndingHabbitStatistic : MonoBehaviour
{
    [SerializeField] private GameObject habbitPrefab;
    [SerializeField] private Transform spawner;
    void Start()
    {
        
    }

    public void SetUpTopHabbit()
    {
        for(int i = 1; i<=3; i++)
        {

            var x = Instantiate(habbitPrefab, spawner.position, Quaternion.identity, spawner);
            x.GetComponent<TopHabbit>().SetUp(i);
        }
    }
}
