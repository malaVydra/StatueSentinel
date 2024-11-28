using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLines", menuName = "Dialogue/Dialogue Lines")]
public class DialogueLines : ScriptableObject
{
    [SerializeField] private string[] lines;
    
    public string[] Lines => lines;
}