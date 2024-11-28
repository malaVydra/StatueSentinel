using UnityEngine;

[System.Serializable]
public class Item
{
    public static int MAX_STACK = 5;

    [SerializeField] private ItemData itemData;
    [SerializeField] private int itemCount;
    public int ItemCount => itemCount;
    public ItemData ItemData => itemData;
    public Item(ItemData _itemData, int _itemCount)
    {
        itemData = _itemData;
        itemCount = _itemCount;
    }
    public void AddToStack(int _amount)
    {
        itemCount += _amount;
    }
    public void RemoveFromStack(int _amount)
    {
        itemCount -= _amount;
    }
    public bool CanAddToStack()
    {
        return itemCount < MAX_STACK;
    }
    public bool CompareItemType(ItemType _type)
    {
        return itemData.ItemType == _type;
    }
}

[System.Serializable]
public struct ItemSavable
{
    public string ItemID;
    public int ItemCount;
    public ItemSavable(Item _item)
    {
        ItemID = _item.ItemData.ItemID;
        ItemCount = _item.ItemCount;
    }
}