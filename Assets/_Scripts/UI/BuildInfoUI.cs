using System.Collections.Generic;
using UnityEngine;

public class BuildInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotsParent;
    
    public void SetSlots(List<Item> _recipe)
    {
        for (int i = 0; i < _recipe.Count; i++)
        {
            ItemSlotUI itemSlotUI = Instantiate(slotPrefab, slotsParent).GetComponent<ItemSlotUI>();
            itemSlotUI.SetItem(_recipe[i]);
        }
    }
}