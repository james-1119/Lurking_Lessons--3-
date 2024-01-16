using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp3 : MonoBehaviour, IInventoryItem // give the key a type IIventoryItem so it is treated as a inventory item
{
    public FirstPersonControllerCustom player; // call the player control
    

    public string Name{ // give the inventory item a name
        get{
            return "Key3";
        }
    }

    public Sprite _Image = null;
    
    public Sprite Image{ // set the sprite
        get {
            return _Image;
        }
    }

    public void OnPickup(){ // function to run when object is picked up
        
        gameObject.SetActive(false); // turn the model of object off
    }

    public void OnDrop(){ // function to run when the object was dropped
        
        gameObject.SetActive(true); // turn the model of object on
        if (player != null)
        {
            Camera playerCamera = player.GetComponentInChildren<Camera>(); // get the location of the player camera

            if (playerCamera != null) // These code will make the new model appears in front of the player
            {
                float distance = 2.0f; // Adjust this value as needed
                Vector3 newPosition = playerCamera.transform.position + playerCamera.transform.forward * distance;

                // Set the object's rotation to match the camera's rotation
                Quaternion newRotation = playerCamera.transform.rotation;

                // Set the game object's position and rotation
                transform.position = newPosition;
                transform.rotation = newRotation;
            }
            else
            {
                Debug.LogWarning("Player camera not found!");
            }
        }
        
    }
}
