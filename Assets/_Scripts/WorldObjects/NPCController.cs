using UnityEngine;

public class NPCController : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject infoUi;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private NPC npcData;
    
    private GameObject infoObject;
    public void Interact()
    {
        dialogueManager.StartDialogue(npcData);
    }
    public void ShowInteractableUI()
    {
        Transform parent = FindObjectOfType<Canvas>().transform;
        infoObject = Instantiate(infoUi, parent);
        infoObject.transform.position = 
            Camera.main.WorldToScreenPoint((Vector2)transform.position + Vector2.up * 2);
    }
    public void HideInteractableUI()
    {
        Destroy(infoObject);
    }
}
