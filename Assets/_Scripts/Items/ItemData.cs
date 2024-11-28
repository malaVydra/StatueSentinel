using UnityEngine;

public enum ItemType
{
    Tool,
    Resource
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private string itemID;
    [SerializeField] private string itemName;
    [SerializeField][Multiline] private string itemDescription;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private bool isStackable;
    [Space]
    [SerializeField] private GameObject itemPrefab;

    [Space]
    [Header("Tool Specifics")]
    [SerializeField] [Range(1f, 10f)] private float toolEfficiency;
    
    #region Public Properties
    public ItemType ItemType => ItemType;
    public string ItemID => itemID;
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemSprite => itemSprite;
    public bool IsStackable => isStackable;
    public GameObject ItemPrefab => itemPrefab;
    
    //Get only if item is a tool
    public float ToolEfficiency => itemType == ItemType.Tool ? toolEfficiency : 0;
    #endregion
}
