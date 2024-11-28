using System.Collections.Generic;
using UnityEngine;

public class BuildingPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventoryManager playerInventoryManager;
    [SerializeField] private BuildingData buildingData;
    [SerializeField] private GameObject popUpUIPrefab, buildingInfoPrefab;

    private BuildInfoUI buildInfoUI;
    
    private bool buildingActive = false;
    public void Interact()
    {
        TryToBuild();
    }
    private void TryToBuild()
    {
        if (!HasSufficientResources()) return;

        Build();
    }

    private bool HasSufficientResources()
    {
        foreach (Item _resource in buildingData.RecipeList)
        {
            List<Item> allRequiredItemsInInventory = 
                playerInventoryManager.PlayerInventory.Items.FindAll(_item => _item.ItemData.ItemID == _resource.ItemData.ItemID);

            int sum = 0;
            foreach (Item item in allRequiredItemsInInventory)
            {
                sum += item.ItemCount;
                if(sum >= _resource.ItemCount) break;
            }
            
            if(sum < _resource.ItemCount)
            {
                Debug.LogWarning("No enough resources to build this building");

                Canvas parent = FindObjectOfType<Canvas>();
                
                Transform popUpUI = Instantiate(popUpUIPrefab, parent.transform).transform;
                popUpUI.position = Camera.main.WorldToScreenPoint(transform.position);
                popUpUI.transform.SetSiblingIndex(0);
                return false;
            }
        }

        return true;
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

        gameObject.SetActive(false);
    }
    public void ShowInteractableUI()
    {
        Transform parent = FindObjectOfType<Canvas>().transform;
        buildInfoUI = Instantiate(buildingInfoPrefab, parent).GetComponent<BuildInfoUI>();
        buildInfoUI.transform.position = Camera.main.WorldToScreenPoint((Vector2)transform.position + Vector2.up * 2);
        buildInfoUI.SetSlots(buildingData.RecipeList);
    }
    public void HideInteractableUI()
    {
        if(buildInfoUI == null) return;
        
        Destroy(buildInfoUI.gameObject);
    }
}
