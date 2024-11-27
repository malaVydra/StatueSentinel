using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class ItemSlotIconUI : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private ItemSlotUI itemSlotUI;
    private Image image;
    private RectTransform rectTransform;

    private void Awake()
    {
        image = GetComponent<Image>();
        itemSlotUI = transform.parent.GetComponent<ItemSlotUI>();
        rectTransform = GetComponent<RectTransform>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Begun");
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }

    public void ChangeSlot(ItemSlotUI _newItemSlotUI)
    {
        transform.SetParent(itemSlotUI.transform);
        rectTransform.localPosition = Vector2.zero;
        image.raycastTarget = true;
        
        if(_newItemSlotUI == itemSlotUI) return;
        
        Item cachedItem = itemSlotUI.Item;
        
        if (_newItemSlotUI.Item != null)
        {
            itemSlotUI.SetItem(_newItemSlotUI.Item);
        }
        else
        {
            itemSlotUI.ClearSlot();
        }
        
        _newItemSlotUI.SetItem(cachedItem);
    }
}