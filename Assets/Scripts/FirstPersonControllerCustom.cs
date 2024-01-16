using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterController))]

public class FirstPersonControllerCustom : MonoBehaviour
{
    // Initializing all the variable
    public float runmul = 0; // player run multiplier
    public float walkingSpeed = 7f; // player walk speed
    public float runningSpeed = 10f; // player run speed
    public float gravity = 20.0f; // gravity
    public Camera playerCamera; // call player camera
    public bool canHide = false;
    public float lookSpeed = 2.0f; // camera turning speed and distance
    public float lookXLimit = 45.0f;
    public StaminaController _staminaController; // call stamina control class
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero; // player position, x, y, z
    float rotationX = 0; // camera rotation
    bool canRun = true; // player can run at start
    public Inventory inventory; // call inventory
    public bool ItemAdded = false;
    public bool walksoundPlay = false;
    public AudioClip walkingSound;
    public AudioSource audioSource;
    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        _staminaController = GetComponent<StaminaController>();
        characterController = GetComponent<CharacterController>();
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked; // on start, player cursor disappear and lock in the middle
        Cursor.visible = false;
    }

    public void SetRunSpeed(float speed){
        runningSpeed = speed;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){ // detect if the player is colliding with an inventory object

        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        
        if(item != null){// add the item to the inventory
            ItemAdded = true;
            inventory.AddItem(item);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("hidePlace")){ // When player collide with hide place collision box, player can hide
            canHide = true;
        }
        if (other.CompareTag("ExitDoor") && inventory.key3Selected == true){ // when player has the escape key and it's near the exit door, player win the game
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("EscapeScene");
        }
    }

    private void OnTriggerExit(Collider other){ // Player can not hide after leaving the hide place collider box
        if (other.CompareTag("hidePlace")){
            canHide = false;
        }
    }

    void Update()
    {  


        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        

        if (!isRunning){
            _staminaController.weAreSprinting = false;
        }

        if (isRunning){// while player has stamina and is trying to run, call Sprint method in stamina controller calss
            if(_staminaController.playerStamina>0){
                _staminaController.weAreSprinting = true;
                _staminaController.Sprinting();
            }
            
        } else {
            isRunning = false;
        }

        if(runmul <= 5 && isRunning == true)// if player is running, use the speed multiplier to smoothly raise player's speed
        {
            if(canRun == true){
                runmul = runmul + 0.25f;
            }
            else{
                runmul = 0;
            }
                
        }
        if (_staminaController.playerStamina <= 1){ // if the player stamina is empty, they can no longger run until the stamina recharge to full
            runmul = 0;
            canRun = false;
            _staminaController.sliderCanvasGroup.alpha = 0;
        }
        if (_staminaController.playerStamina >= 99){
            canRun = true;
        }
        if(isRunning == false) { runmul = 0; }
        float curSpeedX = canMove ? (isRunning ? walkingSpeed+runmul : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? walkingSpeed+runmul : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        moveDirection.y = movementDirectionY;

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            audioSource.clip = walkingSound;
            if(!audioSource.isPlaying){audioSource.Play();}
            
        }
        else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            audioSource.clip = walkingSound;
            audioSource.Stop();
        }

    }
}