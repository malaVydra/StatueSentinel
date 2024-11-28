using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerHoldingItem playerHoldingItem;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private HitboxComponent hitboxComponent;
    
    private bool canAttack = false;

    private void Awake()
    {
        hitboxComponent = GetComponent<HitboxComponent>();
        playerHoldingItem.HoldingItemChanged += OnHoldingItemChanged;
    }
    private void OnHoldingItemChanged(Item _item)
    {
        TryEnableCombat();
    }
    private void Start()
    {
        TryEnableCombat();
    }
    private void Update()
    {
        transform.position = player.transform.position;
        
        if(!canAttack) return;
        
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            weaponAnimator.SetBool("usingTool", true);
            hitboxComponent.SetActive(true, playerHoldingItem.Item.ItemData.ToolEfficiency);
        }
        else if (Keyboard.current.fKey.isPressed)
        {
            hitboxComponent.SetActive(true, playerHoldingItem.Item.ItemData.ToolEfficiency);
        }
        else
        {
            DeactivateCombat();
        }
    }
    private void TryEnableCombat()
    {
        if (playerHoldingItem.Item == null || playerHoldingItem.Item.ItemData.ItemType != ItemType.Weapon)
        {
            DeactivateCombat();
            canAttack = false;
        }
        else
        {
            hitboxComponent.SetActive(true, playerHoldingItem.Item.ItemData.ToolEfficiency);
            canAttack = true;
        }
    }
    private void DeactivateCombat()
    {
        weaponAnimator.SetBool("usingTool", false);
        hitboxComponent.SetActive(false, 0);
    }
}
