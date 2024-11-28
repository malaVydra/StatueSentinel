using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    
    private Coroutine dialogueCoroutine;
    
    private NPC npc;

    private int dialogueIndex;
    
    private bool dialogueActive = false;
    private bool isTyping = false;
    
    public void StartDialogue(NPC _npc)
    {
        if(dialogueActive) return;
        else if (dialogueIndex != 0)
        {
            dialogueIndex = 0;
            return;
        }
        
        playerMovement.EnableMovement(false);
        dialogueActive = true;
        
        dialoguePanel.SetActive(true);
        npc = _npc;
        
        nameText.text = _npc.NPCName;
        NextLine();
    }
    private void Update()
    {
        if(!dialogueActive) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (isTyping)
            {
                StopCoroutine(dialogueCoroutine);
                
                dialogueText.text = npc.DialogueLines.Lines[dialogueIndex];
                isTyping = false;
            }
            else
            {
                dialogueIndex++;
                
                if (dialogueIndex < npc.DialogueLines.Lines.Length)
                {
                    NextLine();
                }
                else
                {
                    playerMovement.EnableMovement(true);
                    dialogueActive = false;
                    dialoguePanel.SetActive(false);
                }
            }
        }
    }
    private void NextLine()
    {
        isTyping = true;
        dialogueCoroutine = StartCoroutine(DialogueAppear(npc.DialogueLines));
    }
    private IEnumerator DialogueAppear(DialogueLines _dialogue)
    {
        dialogueText.text = "";
        foreach (char letter in _dialogue.Lines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
