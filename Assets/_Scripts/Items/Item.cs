using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public static int MAX_STACK = 5;

    private ItemData itemData;
    private int itemCount;
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
}
