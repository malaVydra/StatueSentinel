using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    public UnityEvent<Item> ItemAddedEvent;
    public UnityEvent<Item> ItemRemovedEvent;
    
    private List<Item> items;
    public List<Item> Items => items;

    public Inventory()
    {
        items = new List<Item>();
        ItemAddedEvent = new UnityEvent<Item>();
        ItemRemovedEvent = new UnityEvent<Item>();
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
            ItemAddedEvent?.Invoke(items[i]);
            
            return true;
        }

        return false;
    }
    public bool CanAddItem(Item _item, int _maxDifferentItems)
    {
        //Check if inventory is full
        if (items.Count >= _maxDifferentItems)
        {
            //Check if item is stackable and if there's already one in the inventory
            if (IsItemStackableInInventory(_item)) return true;
            
            return false;
        }
        return true;
    }
    private bool IsItemStackableInInventory(Item _item)
    {
        bool canAddItem = items.Find(x => x.ItemData.ItemID == _item.ItemData.ItemID && x.CanAddToStack()) != null;
        if (_item.ItemData.IsStackable && canAddItem)
        {
            return true;
        }

        return false;
    }
    public void RemoveItem(Item _item)
    {
        if (_item.ItemData.IsStackable)
        {
            HandleStackableItemRemoval(_item);
        }
        else
        {
            Item itemToRemove = items.First(x => x.ItemData.ItemID == _item.ItemData.ItemID);
            itemToRemove.RemoveFromStack(1);
            ItemRemovedEvent?.Invoke(itemToRemove);
        }
    }
    private void HandleStackableItemRemoval(Item _item)
    {
        Item itemToRemove = items.First(x => x.ItemData.ItemID == _item.ItemData.ItemID);
        int itemToRemoveRemaining = itemToRemove.ItemCount - _item.ItemCount;
            
        if (itemToRemoveRemaining > 0)
        {
            itemToRemove.RemoveFromStack(_item.ItemCount);
        }
        else if(itemToRemoveRemaining == 0)
        {
            itemToRemove.RemoveFromStack(_item.ItemCount);
            items.Remove(itemToRemove);
        }
        else
        {
            itemToRemove.RemoveFromStack(itemToRemove.ItemCount);
            items.Remove(itemToRemove);
                
            Item removeItem = new Item(_item.ItemData, Mathf.Abs(itemToRemoveRemaining));
            RemoveItem(removeItem);
        }
            
        ItemRemovedEvent?.Invoke(itemToRemove);
    }
}
