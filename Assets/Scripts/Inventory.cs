using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 10; // this is the maximum slot in inventory

    public string previousSelection = null;

    public int itemSelection = 0;
    public Camera lockerCamera;
    public GameObject game1; 
    public GameObject game2; 
    public GameObject Player;
    public enemyAIPatrol enemyAI;
    public doorScript doorScript;
    [SerializeField] Transform lockerTarget;

    public FirstPersonControllerCustom firstPersonControllerCustom;
    private LimitedVisionEffect limitedVisionEffect;

    private List<IInventoryItem> mItems = new List<IInventoryItem>(new IInventoryItem[10]); // Empty list to store all the item
    public event EventHandler<InventoryEventArgs> ItemAdded; // Initialze a event handler funciton to add item into the inventory

    public event EventHandler<InventoryEventArgs> ItemRemoved; // Initialze a event handler funciton to remove item from the inventory

    public bool flashLightSelected = false; // Initialize flashlight selection
    public bool key1Selected = false;
    public bool key2Selected = false;
    public bool key3Selected = false;
    public bool keySelected = false;

    public int barSelect = 0;

    public string slot = "";

    public string printSlot = "";

    [SerializeField] Animator myAnimationController;

    private void Start(){
        enemyAI = game2.GetComponent<enemyAIPatrol>();
        lockerCamera.enabled = false;
        firstPersonControllerCustom = game1.GetComponent<FirstPersonControllerCustom>();
        limitedVisionEffect = FindObjectOfType<LimitedVisionEffect>(); // Assuming there's only one LimitedVisionEffect in the scene

        //playerCamera = GameObject.Find("Main Camera").GetComponent<FirstPersonControllerCustom> ();

    }

    private void Update(){ 
        
        // Select item effect
        if(Input.GetKeyDown(KeyCode.Alpha1)){ // on button 1 press, the player will select inventory item 1
            barSelect = 1;
        } 

        if(Input.GetKeyDown(KeyCode.Alpha2)){ // do the same for all other buttons
            barSelect = 2;
        } 
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            barSelect = 3;
        } 

        if(Input.GetKeyDown(KeyCode.Alpha4)){
            barSelect = 4;
        } 

        if(Input.GetKeyDown(KeyCode.Alpha5)){
            barSelect = 5;
        } 

        if(Input.GetKeyDown(KeyCode.Alpha6)){
            barSelect = 6;
        } 

        if(Input.GetKeyDown(KeyCode.Alpha7)){
            barSelect = 7;
        } 

        if(Input.GetKeyDown(KeyCode.Alpha8)){
            barSelect = 8;
        }

        if(Input.GetKeyDown(KeyCode.Alpha9) && mItems.Count > 8){
            barSelect = 9;
        } 

        if(Input.GetKeyDown(KeyCode.Alpha0) && mItems.Count > 9){
            barSelect = 10;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0){ // Get the value return from scrolling wheel, and change the barselection
            barSelect++;
            if(barSelect == 11){
                barSelect = 1;
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0){ // Work the same in a opposite direction
            barSelect--;
            if(barSelect == 0){
                barSelect = 10;
            }
        }
        switch (barSelect)
        {
            case 1:
                slot = "Slot";
                printSlot = "Select bar 1";
                itemSelection = 0;
                break;
            case 2:
                slot = "Slot (1)";
                printSlot = "Select bar 2";
                itemSelection = 1;
                break;
            case 3:
                slot = "Slot (2)";
                printSlot = "Select bar 3";
                itemSelection = 2;
                break;
            case 4:
                slot = "Slot (3)";
                printSlot = "Select bar 4";
                itemSelection = 3;
                break;
            case 5:
                slot = "Slot (4)";
                printSlot = "Select bar 5";
                itemSelection = 4;
                break;
            case 6:
                slot = "Slot (5)";
                printSlot = "Select bar 6";
                itemSelection = 5;
                break;
            case 7:
                slot = "Slot (6)";
                printSlot = "Select bar 7";
                itemSelection = 6;
                break;
            case 8:
                slot = "Slot (7)";
                printSlot = "Select bar 8";
                itemSelection = 7;
                break;
            case 9:
                slot = "Slot (8)";
                printSlot = "Select bar 9";
                itemSelection = 8;
                break;
            case 10:
                slot = "Slot (9)";
                printSlot = "Select bar 10";
                itemSelection = 9;
                break;
        }

        if (previousSelection != null)
        {
            Transform previousItem = transform.Find(previousSelection);
            previousItem.GetComponent<Image>().color = new Color32(212, 199, 199, 255);
            if(previousSelection != slot){
                myAnimationController.SetTrigger("SwitchSlot");
            }
        }
        // hiding place
        if(Input.GetKeyDown(KeyCode.Q) && firstPersonControllerCustom.canHide == true){ // press q to hide
                if(!enemyAI.playerInHidingPlace){
                    enemyAI.playerInHidingPlace = true;
                    lockerCamera.enabled = true;
                    Player.SetActive(false);
                    limitedVisionEffect.SetHidingState(true); // Set hiding state to true
                    lockerCamera.transform.position = new Vector3(lockerTarget.position.x, lockerTarget.transform.position.y+2, lockerTarget.position.z); //Position locker camera to a different place
                    doorScript.intText.SetText("Press [Q] to exit hiding place");
                } else {
                    enemyAI.playerInHidingPlace = false;
                    Player.SetActive(true);
                    lockerCamera.enabled = false;
                    limitedVisionEffect.SetHidingState(false); // Set hiding state to false

                }
                
        } else if (firstPersonControllerCustom.canHide != true){
            enemyAI.playerInHidingPlace = false;
        }


        Transform selectItem = transform.Find(slot);
        previousSelection = slot;

        selectItem.GetComponent<Image>().color = new Color32(255, 255, 163, 100);
        //Debug.Log(printSlot);


        // Press E to remove item from your list
        if (Input.GetKeyDown(KeyCode.E)){
            if (mItems[itemSelection] != null){ // if the item you selected is not empty, remove object
                RemoveItem(mItems[itemSelection]); // Call the remove item function below
                Debug.Log(itemSelection + " removed");
            }
            
        }

        // check if there is flashlight in your hand
        if(mItems[itemSelection] != null && mItems[itemSelection].Name == "FlashLight"){ // Check if the object you are holding is called flashlight
            flashLightSelected = true;
        } else {
            flashLightSelected = false;
        }
        // check if key 1 is in your hand
        if(mItems[itemSelection] != null && mItems[itemSelection].Name == "Key1"){ // Check if the object you are holding is called key1
            key1Selected = true;
            
        } else {
            key1Selected = false;
        }
        if(mItems[itemSelection] != null && mItems[itemSelection].Name == "Key2"){ // Check if the object you are holding is called key2
            key2Selected = true;
        } else {
            key2Selected = false;
        }
        if(mItems[itemSelection] != null && mItems[itemSelection].Name == "Key3"){ // Check if the object you are holding is called key3
            key3Selected = true;
        } else {
            key3Selected = false;
        }
        if(mItems[itemSelection] != null && mItems[itemSelection].Name.Contains("Key")){
            keySelected = true;
        } else{
            keySelected = false;
        }
      
    }

    public void AddItem(IInventoryItem item){ // function to add item into inventory
        
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>(); // Get the collider of the object collide with player
            if(collider.enabled){

                collider.enabled = false; // turn off the collider

                for(int i = 0; i < mItems.Count; i++){ // check for empty spot in Inventory slot and add it to the newest 1
                    if (mItems[i] == null){
                        mItems[i] = item;
                        break;
                    }
                }
                item.OnPickup(); // call the item on pickup funciton in their class
                
                if (ItemAdded != null){
                    ItemAdded(this, new InventoryEventArgs(item)); // preform the event handler function ItemAdded
                }
            }
        
        
    }

    public void RemoveItem(IInventoryItem item){// function to remove item from inventory
 
        if(mItems.Contains(item)){ //check if slot contains the item
            mItems[itemSelection] = null; // turn the position of mItem list to null

            item.OnDrop(); // perform the on drop function in item's class

            Collider collider = (item as MonoBehaviour).GetComponent<Collider>(); // reactivate the collider of the inventory object
            if(collider != null){
                collider.enabled = true;
            }

            if(ItemRemoved != null){ // preform the event handler function ItemRemoved
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
        

    }
}