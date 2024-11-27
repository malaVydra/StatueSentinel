using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    private Inventory playerInventory;

    [SerializeField] private int numberOfSlots = 9;
    private int selectedSlot;
    void Awake()
    {
        playerInventory = new Inventory();
        playerInventory.InventoryChanged.AddListener(OnInventoryChanged);
    }
    public void AddItem(Item item)
    {
        playerInventory.AddItem(item);
    }
    private void OnInventoryChanged()
    {
        //UI Update
        
        //...
    }
}
