using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryManager : MonoBehaviour
{
    private Inventory playerInventory;

    [SerializeField] private InventoryPanelUI inventoryPanelUI;
    
    [SerializeField] private int numberOfSlots = 9;
    private int selectedSlot;
    void Awake()
    {
        playerInventory = new Inventory();
        playerInventory.ItemAddedEvent.AddListener(OnInventoryChanged);

        SetInventoryUI();
    }
    private void Update()
    {
        UpdateSelectedSlot();
    }
    private void UpdateSelectedSlot()
    {
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
        inventoryPanelUI.UpdateHUD(selectedSlot);
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
        playerInventory.AddItem(_item);
    }
    private void OnInventoryChanged(Item _newItem)
    {
        inventoryPanelUI.ReloadInventoryUI(_newItem);
    }
}
