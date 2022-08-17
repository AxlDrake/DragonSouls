using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerAnimatorManager animatorManager;

    PlayerLocomotion playerLocomotion;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;    

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool sprint_input;
    public bool jump_input;    
    
    public float rollInputTimer;   

    public bool rb_input;
    public bool rt_input;

    private void Awake()
    {
        animatorManager = GetComponent<PlayerAnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprinting.performed += i => sprint_input = true;
            playerControls.PlayerActions.Sprinting.canceled += i => sprint_input = false;

            playerControls.PlayerActions.Jump.performed += i => jump_input = true;

            playerControls.PlayerActions.RB.performed += i => rb_input = true;
            playerControls.PlayerActions.RT.performed += i => rt_input = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpingInput();
        HandleAttacksInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;
        
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimator(0, moveAmount, sprint_input);

    }

    private void HandleSprintingInput()
    {        

        if(sprint_input)
        {
            rollInputTimer += Time.deltaTime;            
            playerLocomotion.isSprinting = true;
        }
        else
        {
            if(rollInputTimer > 0 && rollInputTimer < 0.5f)
            {                
                playerLocomotion.isSprinting = false;
                playerLocomotion.isRolling = true;
            }            

            rollInputTimer = 0;            
        }
        //if(sprint_input && moveAmount >= 0.5f)
        //{
        //    playerLocomotion.isSprinting = true;
        //}
        //else
        //{
        //    playerLocomotion.isSprinting = false;
        //}
    }

    private void HandleJumpingInput()
    {
        if(jump_input)
        {
            jump_input = false;
            playerLocomotion.HandleJumping();
        }
    }

    private void HandleAttacksInput()
    {
        if(rb_input)
        {
            rb_input = false;
            playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
        }

        if(rt_input)
        {
            rt_input = false;
            playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
        }
    }
}
