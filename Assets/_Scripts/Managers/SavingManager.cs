using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavingManager : MonoBehaviour
{
    [SerializeField] private List<ItemData> allItems;
    
    public static SavingManager Instance;
    public static bool LoadInventoryAtStart = false;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    #region Save/Load Inventory
    public void SaveInventory(Inventory _inventory)
    {
        List<ItemSavable> itemsToSave = new List<ItemSavable>();
        
        foreach (Item item in _inventory.Items)
        {
            ItemSavable itemToSave = new ItemSavable(item);
            itemsToSave.Add(itemToSave);
        }

        Wrapper<ItemSavable> itemsToSaveWrapper = new Wrapper<ItemSavable>();
        itemsToSaveWrapper.Items = itemsToSave;
        
        File.WriteAllText(Application.persistentDataPath + "inventory", 
            JsonUtility.ToJson(itemsToSaveWrapper));
        Debug.Log("Saving inventory..." + JsonUtility.ToJson(itemsToSaveWrapper));
    }
    public List<Item> LoadInventory(Inventory _inventory)
    {
        Debug.Log("Loading inventory...");
        Wrapper<ItemSavable> itemsToLoad =
            JsonUtility.FromJson<Wrapper<ItemSavable>>(File.ReadAllText(Application.persistentDataPath + "inventory"));

        List<Item> items = new List<Item>();
        foreach (ItemSavable itemSavable in itemsToLoad.Items)
        {
            Debug.Log("Loading item: " + itemSavable.ItemID + " " + itemSavable.ItemCount);
            _inventory.AddItem(new Item(
                allItems.Find(x => x.ItemID == itemSavable.ItemID), itemSavable.ItemCount));
        }

        return items;
    }
    #endregion

    #region  Save / Load Wave
    public void SaveWave(int _wave)
    {
        File.WriteAllText(Application.persistentDataPath + "waveData", _wave.ToString());
        
        Debug.Log("Saving wave..." + JsonUtility.ToJson(_wave));
    }
    public int LoadWave()
    {
        Debug.Log("Loading wave...");
        return int.Parse(File.ReadAllText(Application.persistentDataPath + "waveData"));
    }
    #endregion

    #region Save / Load Well Data
    public void SaveWellData(float _healthComponentHealth)
    {
        File.WriteAllText(Application.persistentDataPath + "wellData.bg", 
            _healthComponentHealth.ToString());
        Debug.Log("Saving wave..." + _healthComponentHealth);
    }
    public float LoadWellHealth()
    {
        return float.Parse(File.ReadAllText(Application.persistentDataPath + "wellData.bg"));
    }
    #endregion
}

[System.Serializable]
public class Wrapper<T>
{
    public List<T> Items;
}
