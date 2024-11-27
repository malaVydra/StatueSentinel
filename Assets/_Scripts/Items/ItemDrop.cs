using UnityEngine;
using DG.Tweening;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private ItemData testingData;
    private Item item;
    private bool canPickUp = false;
    private bool beingPickedUp = false;
    
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetItem(new Item(testingData, 1));
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
            .OnComplete(() => canPickUp = true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerInventoryManager playerInventoryManager) && !beingPickedUp)
        {
            beingPickedUp = true;
            
            // Suck item into the player, and destroy this object after animation is completed
            transform.DOScale(Vector2.zero, .5f).SetEase(Ease.InExpo);
            transform.DOMove(other.transform.position, .5f).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    playerInventoryManager.AddItem(item);
                    Destroy(gameObject);
                });
        }
    }
}
