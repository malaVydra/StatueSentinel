using System.Collections.Generic;
using UnityEngine;

public class BuildingPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventoryManager playerInventoryManager;
    [SerializeField] private BuildingData buildingData;
    public void Interact()
    {
        TryToBuild();
    }
    private void TryToBuild()
    {
        Debug.Log("Trying to build");
        foreach (Item resource in buildingData.RecipeList)
        {
            List<Item> allRequiredItemsInInventory = 
                playerInventoryManager.PlayerInventory.Items.FindAll(_item => _item.ItemData.ItemID == resource.ItemData.ItemID);

            int sum = 0;
            foreach (Item item in allRequiredItemsInInventory)
            {
                sum += item.ItemCount;
                if(sum >= resource.ItemCount) break;
            }
            
            if(sum < resource.ItemCount)
            {
                Debug.Log("No enough resources to build this building");
                return;
            }
        }

        Build();
    }
    private void Build()
    {
        Building building = Instantiate(buildingData.BuildingPrefab, transform.position, Quaternion.identity)
            .GetComponent<Building>();
        building.SetBuildingPoint(this);

        foreach (Item item in buildingData.RecipeList)
        {
            playerInventoryManager.PlayerInventory.RemoveItem(item);
        }
    }
    public void ShowInteractableUI()
    {
        
    }
    public void HideInteractableUI()
    {
        
    }
}
