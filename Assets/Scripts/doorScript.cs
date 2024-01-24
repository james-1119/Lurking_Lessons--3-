using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class doorScript : MonoBehaviour
{
    public float interactionDistance; // Ray interaction distance
    public GameObject game1; 
    public GameObject game2; 
    public NPCSystem NPCSystem;
    private Inventory keyPicked; // Access global variable from inventory class
    public string doorOpenAnimationName, doorCloseAnimationName; // initializing the two animation
    public TextMeshProUGUI intText;
    public enemyAIPatrol enemyAI;
    public Collider other;

    void Start(){
        keyPicked = game1.GetComponent<Inventory>();
        NPCSystem = game2.GetComponent<NPCSystem>();
    }

    void Update(){
        Ray ray = new Ray(transform.position, transform.forward); // Set up the ray for the player
        RaycastHit hit; // Raycast returns true if your ray collide with a collider
        
        if(Physics.Raycast(ray, out hit, interactionDistance)){ // Determine if your ray collide with a collider in a certain interaction
            if(hit.collider.gameObject.tag == "door"){ // If the item tag is door
                GameObject doorParent = hit.collider.transform.parent.parent.gameObject; // Reference the door that is colliding
                Animator doorAnim = doorParent.GetComponent<Animator>(); // Get the animator 
                
                if(doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimationName)){ // Changing the text box base on door's state
                    intText.SetText("Press [R] to Close"); 
                }
                if(doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimationName)){
                        intText.SetText("Press [R] to Open");
                    }
                if(Input.GetKeyDown(KeyCode.R)){
                    if(doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimationName)){ // if door is open play the close animation
                        doorAnim.ResetTrigger("Open");
                        doorAnim.SetTrigger("Close");
                    }
                    if(doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimationName)){ // if door is close play the open animation
                        doorAnim.ResetTrigger("Close");
                        doorAnim.SetTrigger("Open");
                    }
                }
            }  
            if(hit.collider.gameObject.tag == "lockedDoor1"){
                if(keyPicked.key1Selected){ // If the player has the right key in hand, unlock the door by changing it's tag
                    hit.collider.gameObject.tag = "door";
                    intText.SetText("");
                } else {
                    intText.SetText("Door Locked"); // display this when the door is locked
                }
            } 
            if(hit.collider.gameObject.tag == "lockedDoor2"){
                if(keyPicked.key2Selected){
                    hit.collider.gameObject.tag = "door";
                    intText.SetText("");
                } else {
                    intText.SetText("Door Locked");
                }
            } 
            if(hit.collider.gameObject.tag == "lockedDoor3"){
                if(keyPicked.key3Selected){
                    hit.collider.gameObject.tag = "door";
                    intText.SetText("");
                } else {
                    intText.SetText("Exit Door Locked");
                }
            }
            if(hit.collider.gameObject.tag == "HideBody"){ // display text when player look toward a hide place
                if(enemyAI.playerInHidingPlace){ // Display this when the player is in the locker
                    intText.SetText("Press [Q] to exit hiding place");
                } else {
                    intText.SetText("Press [Q] to enter hiding place"); // display this when the the player is not in the locker
                }
            }
        } else if (!Physics.Raycast(ray, out hit, interactionDistance)){
            intText.SetText("");
            
        }
    }
}
