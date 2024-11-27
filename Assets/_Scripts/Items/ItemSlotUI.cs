using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlotUI : MonoBehaviour, IDropHandler
{
    private Item item;
    public Item Item => item;
    
    [SerializeField] private Sprite selectedSprite, hudSprite, defaultSprite;
    private Image slotImage;

    [Header("Component References")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemCount;
    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }
    private void OnMouseEnter()
    {
        if(IsEmpty()) return;
        
        //display item info through UI
    }
    
    #region Slot Selection and Deselection
    public void SelectSlot()
    {
        //item in the slot is selected
    }
    public void DeselectSlot()
    {
        //item in the slot is deselected
    }
    #endregion
    
    public void SetSprite(int _index, int _selectedSlot = 0)
    {
        if (_index == _selectedSlot)
        {
            slotImage.sprite = selectedSprite;
        }
        else if (_index < InventoryPanelUI.HUD_SIZE)
        {
            slotImage.sprite = hudSprite;
        }
        else
        {
            slotImage.sprite = defaultSprite;
        }
    }
    public void SetItem(Item _item)
    {
        item = _item;
        
        itemIcon.sprite = _item.ItemData.ItemSprite;
        itemIcon.gameObject.SetActive(true);

        if (item.ItemData.IsStackable && item.ItemCount > 1)
        {
            itemCount.text = _item.ItemCount.ToString();   
            itemCount.gameObject.SetActive(true);
        }
        else
        {
            itemCount.text = string.Empty;
            itemCount.gameObject.SetActive(false);
        }
    }
    public void ClearSlot()
    {
        item = null;
        itemCount.text = string.Empty;
        
        itemIcon.gameObject.SetActive(false);
        itemCount.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemSlotIconUI itemDropped = eventData.pointerDrag.GetComponent<ItemSlotIconUI>();
        itemDropped.ChangeSlot(this);
    }

    public bool IsEmpty()
    {
        return Item == null;
    }
}
