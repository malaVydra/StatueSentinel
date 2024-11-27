using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    
    private Rigidbody2D playerRigidBody;
    private PlayerInputActions playerInput;
    
    private Vector2 inputVector;
    private void Awake()
    {
        InitializeInput();
        GetComponentReferences();
    }
    private void Update()
    {
        inputVector = playerInput.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        playerRigidBody.velocity = inputVector * speed;
    }
    
    #region Setup Methods
    private void GetComponentReferences()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    private void InitializeInput()
    {
        playerInput = new PlayerInputActions();
        playerInput.Player.Enable();
    }

    #endregion
}
