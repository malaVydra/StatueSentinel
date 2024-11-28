using System.Collections.Generic;
using UnityEngine;

public class DropComponent : MonoBehaviour
{
    [SerializeField] private string dropTag = "Drops";
    [SerializeField] private GameObject dropItemPrefab;
    
    [Header("Drop Rate")]
    [SerializeField] [Range(1, 3)] private int minimumDrops = 1;
    [SerializeField] [Range(3, 10)] private int maximumDrops = 3;
    [SerializeField] private List<ItemData> possibleDrops;

    public void Drop()
    {
        Transform parent = GameObject.FindGameObjectWithTag(dropTag).transform;
        
        int drops = Random.Range(minimumDrops, maximumDrops);
        for (int i = 0; i < drops; i++)
        {
            ItemDrop itemDrop = Instantiate(dropItemPrefab, parent).GetComponent<ItemDrop>();
            itemDrop.transform.position = transform.position;
            
            Item itemToDrop = new Item(possibleDrops[Random.Range(0, possibleDrops.Count)], 1);
            itemDrop.SetItem(itemToDrop);
        }
    }
}
