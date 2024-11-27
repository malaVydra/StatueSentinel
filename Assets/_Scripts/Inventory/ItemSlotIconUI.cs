using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class ItemSlotIconUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TMP_Text itemCount;
    
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
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {        
        transform.SetParent(itemSlotUI.transform);
        rectTransform.localPosition = Vector2.zero;
        image.raycastTarget = true;
    }
    public void ChangeSlot(ItemSlotUI _newItemSlotUI)
    {
        transform.SetParent(itemSlotUI.transform);
        rectTransform.localPosition = Vector2.zero;
        image.raycastTarget = true;
        
        if(_newItemSlotUI == itemSlotUI) return;
        
        Item cachedItem = itemSlotUI.Item;
        
        if (!_newItemSlotUI.IsEmpty())
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