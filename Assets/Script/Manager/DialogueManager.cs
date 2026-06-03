using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    [SerializeField] private Transform choiceButtonParent;
    [SerializeField] private Button choiceButtonPrefab;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Image BG;
    [SerializeField] private Image charImageLeft;
    [SerializeField] private Image charImageRight;
    [SerializeField] private TMP_Text charName;
    [SerializeField] private TMP_Text textBoxDialogueArea;
    private Queue<DialogueLines> lines;
    private List<DialogueTrigger> choices;
    [SerializeField] bool isDialogueActive = false;
    [SerializeField] float typingSpeed;
    [SerializeField] float typingSpeedFast;
    [SerializeField] float typingSpeedNormal;
    [SerializeField] private GameObject dialogueCanvas;


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
        choices = null;
    }

    public void StartDialogue(Dialogue dialogue, List<DialogueTrigger> dialogueChoices)
    {
        choicePanel.SetActive(false);
        Debug.Log("Dialogue Start");
        isDialogueActive = true;
        dialogueCanvas.SetActive(true);
        //animator.Play("show");
        lines.Clear();
        foreach (DialogueLines dialogueLines in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLines);
        }
        choices = dialogueChoices;
        DisplayNextDialogue();
    }

    private void Update()
    {
        Debug.Log(typingSpeed);
        if (Input.GetKey(KeyCode.Space) && isDialogueActive)
        {
            typingSpeed = typingSpeedFast;
        }
        else
        {
            typingSpeed = typingSpeedNormal;
        }
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
            ShowChar(charImageRight);
            charImageRight.transform.localScale = new Vector2(-1, 1);
            charImageRight.sprite = currLines.character.icon;
            DimImage(charImageLeft);
        }
        else
        {
            ShowChar(charImageLeft);
            charImageLeft.transform.localScale = new Vector2(1, 1);
            charImageLeft.sprite = currLines.character.icon;
            DimImage(charImageRight);
        }
        BG.sprite = currLines.BG;
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
        if (choices != null && choices.Count > 0)
        {
            ShowChoices();
        }
        else
        {
            dialogueCanvas.SetActive(false);
        }
        Debug.Log("Dialogue ends");
    }

    private void ShowChoices()
    {
        choicePanel.SetActive(true);
        foreach (Transform child in choiceButtonParent)
        {
            Destroy(child.gameObject);
        }

        foreach (DialogueTrigger choice in choices)
        {
            Button button =
                Instantiate(choiceButtonPrefab, choiceButtonParent);

            button.GetComponentInChildren<TMP_Text>().text =
                choice.name;

            button.onClick.AddListener(() =>
            {
                foreach (Transform child in choiceButtonParent)
                {
                    Destroy(child.gameObject);
                }
                choice.TriggerDialogue();
            });
        }
    }

    private void DimImage(Image image)
    {
        Color c = image.color;
        c.a = 0.5f;
        image.color = c;
    }

    private void ShowChar(Image image)
    {
        Color c = image.color;
        c.a = 1f;
        image.color = c;
    }
}
