using UnityEngine;
using DG.Tweening;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private ItemData presetData;
    
    private CircleCollider2D circleCollider;
    
    private Item item;
    private bool beingPickedUp = false;
    
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        
        if (presetData != null)
        {
            SetItem(new Item(presetData, 1));
        }
    }
    
    public void SetItem(Item _item)
    {
        item = _item;
        
        LoadItemData();
        DropAnimation();
    }
    private void LoadItemData()
    {
        spriteRenderer.sprite = item.ItemData.ItemSprite;
    }
    private void DropAnimation()
    {
        // Drop to random position, and allow player to pick up after animation is completed
        Vector2 randomDestination = (Vector2)transform.position
                                    + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        transform.DOMove(randomDestination, 1f).SetEase(Ease.OutBack)
            .OnComplete(() => circleCollider.enabled = true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInventoryManager playerInventoryManager)
            && !beingPickedUp
            && playerInventoryManager.CanAddItem(item))
        {
            beingPickedUp = true;
            
            // Suck item into the player, and destroy this object after animation is completed
            transform.DOScale(Vector2.zero, .2f).SetEase(Ease.InExpo);
            transform.DOMove(other.transform.position, .2f).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    playerInventoryManager.AddItem(item);
                    Destroy(gameObject);
                });
        }
    }
}
