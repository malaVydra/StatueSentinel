using UnityEngine;

public class PlayerHoldingItem : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetHoldingItem(Item _item)
    {
        if(_item == null)
        {
            spriteRenderer.sprite = null;
            return;
        }
        
        item = _item;
        spriteRenderer.sprite = item.ItemData.ItemSprite;
    }
}
