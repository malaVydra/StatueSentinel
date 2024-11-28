using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceObject : MonoBehaviour, IDestructible
{
    [HideInInspector] public UnityEvent Destroyed; 
    
    [SerializeField] private List<ItemData> toolsThatCanDestroy;
    [SerializeField] private GameObject destructionUIPrefab;

    private DestructionInfoUI infoUI;
    private HealthComponent healthComponent;
    private DropComponent dropComponent;
    private void Awake()
    {
        dropComponent = GetComponent<DropComponent>();
        healthComponent = GetComponent<HealthComponent>();
        
        healthComponent.HealthLost.AddListener(Destruct);
    }
    public void ShowDestructionUI(bool _show)
    {
        if (_show)
        {
            if (infoUI == null)
            {
                Canvas canvas = FindObjectOfType<Canvas>();
                infoUI = Instantiate(destructionUIPrefab, canvas.transform).GetComponent<DestructionInfoUI>();

                Vector2 offset = Vector2.up * 3;
                infoUI.transform.position = Camera.main.WorldToScreenPoint((Vector2)transform.position + offset);
                
                infoUI.transform.SetSiblingIndex(0);
            }
            else
            {
                infoUI.gameObject.SetActive(true);
            }
            
            infoUI.SetText(toolsThatCanDestroy);
        }
        else
        {
            if(infoUI == null) return;
            
            infoUI.gameObject.SetActive(false);
        }
    }
    public void Damage(float _amount)
    {
        healthComponent.ChangeHealth(-_amount);
    }
    public bool CanDestruct(Item _item)
    {
        if (_item == null) return false;
        
        for (int i = 0; i < toolsThatCanDestroy.Count; i++)
        {
            if (toolsThatCanDestroy[i].ItemID == _item.ItemData.ItemID)
            {
                return true;
            }
        }

        return false;
    }
    public void Destruct()
    {
        Destroyed?.Invoke();
        
        dropComponent.Drop();
        
        Destroy(infoUI.gameObject);
        Destroy(gameObject);
    }
}