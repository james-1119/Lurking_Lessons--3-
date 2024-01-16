using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollisionScript : MonoBehaviour
{ // This script will make enemy open the door when it gets close
    public string doorCloseAnimationName; // calling the door animation state
    private void OnTriggerEnter(Collider other){ // Set up the trigger collider on the door
        GameObject doorParent = transform.parent.parent.gameObject;
        Animator doorAnim = doorParent.GetComponent<Animator>();
        if (other.CompareTag("enemy")){ // when the door collide with an game object with enemy tag, it will play the open animation
            if(doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimationName)){
                doorAnim.ResetTrigger("Close");
                doorAnim.SetTrigger("Open");
            }
        }
    }
}
