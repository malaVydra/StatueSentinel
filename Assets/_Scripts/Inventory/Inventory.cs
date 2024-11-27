using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class Inventory
{
    public UnityEvent InventoryChanged;
    
    private List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
        InventoryChanged = new UnityEvent();
    }
    public void AddItem(Item _item)
    {
        if (_item.ItemData.IsStackable)
        {
            Item stackItem = items.Find(item => item.ItemData.ItemID == _item.ItemData.ItemID 
                                                && item.ItemCount < Item.MAX_STACK);

            if (stackItem == null || stackItem.ItemCount + _item.ItemCount > Item.MAX_STACK)
            {
                items.Add(_item);
                return;
            }
            
            stackItem.AddToStack(1);
        }
        
        items.Add(_item);
    }
}
