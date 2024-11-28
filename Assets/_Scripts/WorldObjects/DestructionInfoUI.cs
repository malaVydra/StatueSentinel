using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestructionInfoUI : MonoBehaviour
{
    private string startingText;
    private TMP_Text infoText;

    private void Awake()
    {
        infoText = GetComponentInChildren<TMP_Text>();
        startingText = infoText.text;
    }

    public void SetText(List<ItemData> _items)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (i == 0)
            {
                infoText.text = startingText + $" {_items[i].ItemName}";
                continue;
            }
            
            infoText.text = startingText + $", {_items[i].ItemName}";
        }
    }
}
