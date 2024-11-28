using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHoldingItem : MonoBehaviour
{
    public Action<Item> HoldingItemChanged;
    
    private Item item;
    private SpriteRenderer spriteRenderer;
    public Item Item => item;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetHoldingItem(Item _item)
    {
        if (_item == item) return;
        
        if(_item != null)
        { 
            item = _item;
            spriteRenderer.sprite = item.ItemData.ItemSprite;
        }
        else
        {
            item = null;
            spriteRenderer.sprite = null;
        }
        
        HoldingItemChanged?.Invoke(_item);
    }
}
