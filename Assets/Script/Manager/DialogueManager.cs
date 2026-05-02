using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private Image charImageLeft;
    [SerializeField] private Image charImageRight;
    [SerializeField] private TMP_Text charName;
    [SerializeField] private TMP_Text textBoxDialogueArea;
    private Queue<DialogueLines> lines;
    [SerializeField] bool isDialogueActive = false;
    [SerializeField] float typingSpeed;
    //[SerializeField] Animator animator;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        lines = new Queue<DialogueLines>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Dialogue Start");
        isDialogueActive = true;
        //animator.Play("show");
        lines.Clear();
        foreach (DialogueLines dialogueLines in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLines);
        }
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialogueLines currLines = lines.Dequeue();
        if (currLines.onTheRight)
        {
            charImageRight.sprite = currLines.character.icon;
        }
        else
        {
            charImageLeft.sprite = currLines.character.icon;
        }

            charName.text = currLines.character.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currLines));
    }

    IEnumerator TypeSentence(DialogueLines dialogueLine)
    {
        textBoxDialogueArea.text = dialogueLine.line;
        textBoxDialogueArea.maxVisibleCharacters = 0;
        for (int i = 0; i <= dialogueLine.line.Length; i++)
        {
            textBoxDialogueArea.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        //animator.Play("hide");
        Debug.Log("Dialogue ends");
    }
}
