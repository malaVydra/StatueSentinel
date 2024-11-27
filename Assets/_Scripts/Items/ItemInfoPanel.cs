using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;

    private Animator animator;
    private Image image;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        image = GetComponent<Image>();
    }
    
    public void DisplayItemInfo(Item _item, Vector2 _position)
    {
        transform.position = _position;
        
        itemName.text = _item.ItemData.ItemName;
        itemDescription.text = _item.ItemData.ItemDescription;
        
        animator.SetTrigger("appear");
    }
    public void HideItemInfo()
    {
        animator.SetTrigger("disappear");
    }
}
