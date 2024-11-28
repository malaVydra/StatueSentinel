using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryManager : MonoBehaviour
{
    private Inventory playerInventory;
    public Inventory PlayerInventory => playerInventory;

    [SerializeField] private int numberOfSlots = 9;
    
    [Header("Component References")]
    [SerializeField] private InventoryPanelUI inventoryPanelUI;
    [SerializeField] private PlayerHoldingItem holdingItem;
    
    private int selectedSlot;
    void Awake()
    {
        playerInventory = new Inventory();
        playerInventory.ItemAddedEvent.AddListener(OnItemAdded);
        playerInventory.ItemRemovedEvent.AddListener(OnItemRemoved);
        
        SetInventoryUI();
    }
    private void Start()
    {
        ItemSlotUI.OnAnySlotChanged += SyncHUDWithSelectedSlot;
    }

    private void Update()
    {
        UpdateSelectedSlot();
    }
    private void UpdateSelectedSlot()
    {
        // Scrolling Logic - selecting slots in HUD
        Vector2 mouseScroll = Mouse.current.scroll.ReadValue();
        if(mouseScroll.y == 0) return;
        
        if (mouseScroll.y > 0)
        {
            selectedSlot--;
            if (selectedSlot < 0)
            {
                selectedSlot = InventoryPanelUI.HUD_SIZE - 1;
            }
        }
        else if (mouseScroll.y < 0)
        {
            selectedSlot++;
            if (selectedSlot >= InventoryPanelUI.HUD_SIZE)
            {
                selectedSlot = 0;
            }
        }
        
        SyncHUDWithSelectedSlot();
    }

    private void SyncHUDWithSelectedSlot()
    {
        inventoryPanelUI.UpdateHUD(selectedSlot);
        holdingItem.SetHoldingItem(inventoryPanelUI.GetSelectedItem());
    }

    private void SetInventoryUI()
    {
        for(int i = 0; i < numberOfSlots; i++)
        {
            inventoryPanelUI.AddSlot(i);
        }
    }
    public bool CanAddItem(Item _item)
    {
        return playerInventory.CanAddItem(_item, numberOfSlots);
    }
    public void AddItem(Item _item)
    {
        if (!CanAddItem(_item))
        {
            return;
        }
        
        playerInventory.AddItem(_item);
    }
    private void OnItemRemoved(Item _removedItem)
    {
        inventoryPanelUI.ItemRemovedUpdateUI(_removedItem);

        inventoryPanelUI.UpdateHUD(selectedSlot);
        holdingItem.SetHoldingItem(inventoryPanelUI.GetSelectedItem());
    }
    private void OnItemAdded(Item _newItem)
    {
        inventoryPanelUI.ItemAddedUpdateUI(_newItem);
        inventoryPanelUI.UpdateHUD(selectedSlot);
        
        holdingItem.SetHoldingItem(inventoryPanelUI.GetSelectedItem());
    }
}
