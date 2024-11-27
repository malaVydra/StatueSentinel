using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;

public class InventoryPanelUI : MonoBehaviour
{
    public static int HUD_SIZE = 3;

    [SerializeField] private ItemSlotUI slotPrefabUI;
    private List<ItemSlotUI> itemSlots = new List<ItemSlotUI>();
    private ItemSlotUI selectedSlot;
    
    private bool inventoryOpen = false;
    
    private Animator animator;
    private Transform slotParent;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        slotParent = transform.GetChild(0);
    }

    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            if (inventoryOpen)
            {
                ShowInventory(false);
                return;
            }
            ShowInventory(true);
        }
        else if (Keyboard.current.escapeKey.wasPressedThisFrame && inventoryOpen)
        {
            ShowInventory(false);
        }
    }
    public void AddSlot(int _index)
    {
        ItemSlotUI itemSlotUI = Instantiate(slotPrefabUI, slotParent);
        itemSlotUI.SetSprite(_index);
        itemSlots.Add(itemSlotUI);
    }
    public void ShowInventory(bool _show)
    {
        inventoryOpen = _show;
        animator.SetBool("inventoryShown", _show);
    }
    public void UpdateHUD(int _selectedSlot)
    {
        for(int i = 0; i < HUD_SIZE; i++)
        {
            itemSlots[i].SetSprite(i, _selectedSlot);
        }

        selectedSlot?.DeselectSlot();
        selectedSlot = itemSlots[_selectedSlot];
    }
    public void ReloadInventoryUI(Item _updatedItem)
    {
        ItemSlotUI slotToFill = itemSlots.Find(x => x.Item == _updatedItem);

        if (slotToFill == null)
        {
            slotToFill = itemSlots.FirstOrDefault(x => x.IsEmpty());
        }
        
        slotToFill?.SetItem(_updatedItem);
    }
}
