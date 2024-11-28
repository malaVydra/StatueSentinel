using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC/NPC Data")]
public class NPC : ScriptableObject
{
    [SerializeField] private string npcName;
    [SerializeField] private DialogueLines dialogueLines;
    
    public string NPCName => npcName;
    public DialogueLines DialogueLines => dialogueLines;
}