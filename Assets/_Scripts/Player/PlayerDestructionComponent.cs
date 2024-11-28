using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDestructionComponent : MonoBehaviour
{
    [SerializeField] private PlayerHoldingItem holdingItem;
    
    private Animator animator;
    private PlayerMovement playerMovement;
    
    private bool canDestruct = false;
    private IDestructible objectToDestruct;

    private void Awake()
    {
        animator = holdingItem.GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (!ValidateDestructionConditions())
        {
            ResetPlayerState();
            return;
        }
        HandlePlayerDestruction();
    }
    private void HandlePlayerDestruction()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            playerMovement.EnableMovement(false);
            animator.SetBool("usingTool", true);
            objectToDestruct.ShowDestructionUI(false);
        }
        else if (Keyboard.current.eKey.isPressed)
        {
            float efficiency = holdingItem.Item.ItemData.ToolEfficiency * Time.deltaTime;
            objectToDestruct.Damage(efficiency);
        }
        else if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            ResetPlayerState();
            objectToDestruct.ShowDestructionUI(true);
        }
    }
    private bool ValidateDestructionConditions()
    {
        //Condition 1
        if (!canDestruct || objectToDestruct == null) return false;
        //Condition 2
        if (!objectToDestruct.CanDestruct(holdingItem.Item)) return false;

        return true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDestructible destructible))
        {
            canDestruct = true;
            objectToDestruct = destructible;
            objectToDestruct.ShowDestructionUI(true);
        }
    }
    private void OnDestroy()
    {
        playerMovement.EnableMovement();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDestructible _))
        {
            if(objectToDestruct == null) return;
            
            objectToDestruct.ShowDestructionUI(false);
            canDestruct = false;
            objectToDestruct = null;
            ResetPlayerState();
        }
    }
    private void ResetPlayerState()
    {
        if (animator.GetBool("usingTool"))
        {
            playerMovement.EnableMovement();
            animator.SetBool("usingTool", false);
        }
    }
}
