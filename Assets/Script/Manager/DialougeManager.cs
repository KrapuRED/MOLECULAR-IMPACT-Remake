using TMPro;
using UnityEngine;

public class DialougeManager : MonoBehaviour
{
    public TMP_Text text;

    private void Update()
    {
        text.maxVisibleCharacters = 0;
    }
}
