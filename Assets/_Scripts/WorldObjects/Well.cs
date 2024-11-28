using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventoryManager playerInventoryManager;
    [SerializeField] private GameObject popUpUIPrefab, fixingInfoPrefab;
    [SerializeField] private BuildingData fixingWellData;
    
    private BuildInfoUI buildInfoUI;
    
    private HealthComponent healthComponent;
    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.HealthLost.AddListener(DestroyWell);
        
        GameManager.Instance.GameSave.AddListener(SaveWell);

        if (SavingManager.LoadInventoryAtStart)
        {
            healthComponent.SetHealth(SavingManager.Instance.LoadWellHealth());
        }
    }
    private void SaveWell()
    {
        SavingManager.Instance.SaveWellData(healthComponent.Health);
    }
    private void DestroyWell()
    {
        GameManager.Instance.GameOver();
    }
    
    private bool HasSufficientResources()
    {
        foreach (Item _resource in fixingWellData.RecipeList)
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

    public void Interact()
    {
        FixWell();
    }

    private void FixWell()
    {
        if (!HasSufficientResources()) return;
        
        foreach (Item item in fixingWellData.RecipeList)
        {
            playerInventoryManager.PlayerInventory.RemoveItem(item);
        }
        
        healthComponent.ChangeHealth(5);
    }

    public void ShowInteractableUI()
    {
        Transform parent = FindObjectOfType<Canvas>().transform;
        buildInfoUI = Instantiate(fixingInfoPrefab, parent).GetComponent<BuildInfoUI>();
        buildInfoUI.transform.position = Camera.main.WorldToScreenPoint((Vector2)transform.position + Vector2.up * 5);
        buildInfoUI.SetSlots(fixingWellData.RecipeList);
    }
    public void HideInteractableUI()
    {
        if(buildInfoUI == null) return;
        
        Destroy(buildInfoUI.gameObject);
    }
}