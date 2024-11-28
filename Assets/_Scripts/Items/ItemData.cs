using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Item Data")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemID;
    [SerializeField] private string itemName;
    [SerializeField][Multiline] private string itemDescription;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private bool isStackable;
    [Space]
    [SerializeField] private GameObject itemPrefab;
    
    #region Public Properties
    public string ItemID => itemID;
    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemSprite => itemSprite;
    public bool IsStackable => isStackable;
    public GameObject ItemPrefab => itemPrefab;
    #endregion
}
