using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    public UnityEvent<Item> ItemAddedEvent;
    
    private List<Item> items;
    public List<Item> Items => items;

    public Inventory()
    {
        items = new List<Item>();
        ItemAddedEvent = new UnityEvent<Item>();
    }
    public void AddItem(Item _item)
    {
        if (_item.ItemData.IsStackable)
        {
            if (AddItemToStackIfPossible(_item)) return;
        }
        
        items.Add(_item);
        ItemAddedEvent?.Invoke(_item);
    }
    private bool AddItemToStackIfPossible(Item _item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ItemData.ItemID != _item.ItemData.ItemID || items[i].ItemCount >= Item.MAX_STACK) continue;
                
            items[i].AddToStack(1);
            Debug.Log("Current number of stackable items in slot: " + items[i].ItemCount);
            ItemAddedEvent?.Invoke(items[i]);
            
            return true;
        }

        return false;
    }
    public bool CanAddItem(Item _item, int _maxDifferentItems)
    {
        if(items.Find(x => x.ItemData.ItemID == _item.ItemData.ItemID) != null)
        {
            return true;
        }
        return items.Count < _maxDifferentItems;
    }
}
