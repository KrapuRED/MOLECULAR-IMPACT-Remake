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

    [Header("=== Choice Button ===")]
    [SerializeField] private Transform choiceButtonParent;
    [SerializeField] private Button choiceButtonPrefab;
    [SerializeField] private CanvasGroup choicePanel;

    [Header("=== Background Image ===")]
    [SerializeField] private Image backgroundImage;

    [Header("=== Character ===")]
    [SerializeField] private Image charImageLeft;
    [SerializeField] private Image charImageRight;
    [SerializeField] private TMP_Text charName;

    [Header("Text Box Area")]
    [SerializeField] private TMP_Text textBoxDialogueArea;
    private Queue<DialogueLines> lines;
    private List<DialogueTrigger> choices;

    [Header("=== Typing Speed ===")]
    private float typingSpeed;
    [SerializeField] private float typingSpeedFast;
    [SerializeField] private float typingSpeedNormal;

    [Header("=== Dialogue Panel ===")]
    [SerializeField] private CanvasGroup dialoguePanel;

    [Header("=== Check Only ===")]
    [SerializeField] bool isDialogueActive = false;
    public bool IsDialogueActive =>  isDialogueActive;


    //[SerializeField] Animator animator;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        choicePanel.alpha = 0;
        HideDialoguePanel();
    }

    public void StartDialogue(Dialogue dialogue, List<DialogueTrigger> dialogueChoices)
    {
        choicePanel.alpha = 0;
        Debug.Log("Dialogue Start");
        isDialogueActive = true;
        ShowDialoguePanel();
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
        //Debug.Log(typingSpeed);
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
        backgroundImage.sprite = currLines.backGroundImage;
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
            HideDialoguePanel();
        }
        Debug.Log("Dialogue ends");
    }

    private void ShowChoices()
    {
        choicePanel.alpha = 1;
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
    
    private void HideDialoguePanel()
    {
        dialoguePanel.alpha = 0;
        dialoguePanel.blocksRaycasts = false;
        dialoguePanel.interactable = false;
    }

    private void ShowDialoguePanel()
    {
        dialoguePanel.alpha = 1;
        dialoguePanel.blocksRaycasts = true;
        dialoguePanel.interactable = true;
    }
}
