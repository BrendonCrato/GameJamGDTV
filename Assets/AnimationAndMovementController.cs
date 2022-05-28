using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    PlayerInput playerInput;
    CharacterController characterController;
    Animator animator;

    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    private Vector2 aim;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    bool isMovementPressed;
    bool isRunPressed;
    
    float rotationFactorPerFrame = 15f;
    float runMultiplier = 3.0f;
    public float points;
    public float maxHealth;
    public float health;
    public float movementSpeed;

    void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharacterControls.Move.started += onMovementInput;
         
        playerInput.CharacterControls.Move.canceled += onMovementInput;

        playerInput.CharacterControls.Move.performed += onMovementInput;

        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;

        health = maxHealth;

        ////to lock in the centre of window
        //Cursor.lockState = CursorLockMode.Locked;
        ////to hide the curser
        //Cursor.visible = true;
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * movementSpeed;
        currentMovement.z = currentMovementInput.y * movementSpeed;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }
    
    void handleAim()
    {
        aim = playerInput.CharacterControls.Aim.ReadValue<Vector2>(); 
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }

        if((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float groundedGravity = -.05f;
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovementInput.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovementInput.y;

        Quaternion currentRotation = transform.rotation;

        // creates a new rotation based on where the player is currently pressing
        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

        //Debug.Log(aim); 
        //Ray ray = Camera.main.ScreenPointToRay(aim);
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float rayDistance;

        //if(groundPlane.Raycast(ray, out rayDistance))
        //{
        //    Vector3 point = ray.GetPoint(rayDistance);
        //    LookAt(point);

        //    Quaternion targetRotation = Quaternion.LookRotation(point);
        //    transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        //}

    }

    void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    // Update is called once per frame
    void Update()
    {
        //Plane playerPlane = new Plane(Vector3.up, transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //float hitDist = 0.0f;

        //if (playerPlane.Raycast(ray, out hitDist))
        //{
        //    Vector3 targetPoint = ray.GetPoint(hitDist);
        //    Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        //    targetRotation.x = 0;
        //    targetRotation.z = 0;
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7f * Time.deltaTime);
        //}

        handleGravity();
        //handleAim();
        //handleRotation();
        //handleAnimation();

        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }

        if(health <= 0)
        {
            Die();
        }
    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }

    void Die()
    {

    }
}
