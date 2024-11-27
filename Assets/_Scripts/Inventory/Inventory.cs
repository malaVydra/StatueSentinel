using System.Collections.Generic;
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
        ItemAddedEvent?.Invoke(_item);
        
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

    public bool CanAddItem(Item _item, int _maxDifferentItems)
    {
        if(items.Find(x => x.ItemData.ItemID == _item.ItemData.ItemID) != null)
        {
            return true;
        }
        return items.Count < _maxDifferentItems;
    }
}
