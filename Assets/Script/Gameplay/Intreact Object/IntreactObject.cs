using UnityEngine;

public class IntreactObject : MonoBehaviour
{
    [SerializeField] protected string objectID;

    public virtual void OnIntreactObject()
    {
        Debug.Log("Intreact with object: " + objectID);
    }
}
