using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private Animator playerAnimator;
    
    private Rigidbody2D playerRigidBody;
    private PlayerInputActions playerInput;
    
    private Vector2 inputVector;

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }
    private void OnDisable()
    {
        playerInput.Player.Disable();
    }
    private void Awake()
    {
        playerInput = new PlayerInputActions();
        GetComponentReferences();
    }
    private void Update()
    {
        inputVector = playerInput.Player.Move.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        playerRigidBody.velocity = inputVector * speed;
        
        if (inputVector == Vector2.zero)
        {
            playerAnimator.SetBool("isMoving", false);
            return;
        }
        playerAnimator.SetBool("isMoving", true);
    }

    #region Setup Methods
    private void GetComponentReferences()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    #endregion
}
