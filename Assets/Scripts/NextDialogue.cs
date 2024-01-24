using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialogue : MonoBehaviour
{
    int index = 2; 
    public FirstPersonControllerCustom playerControl;
    public GameObject game;
    void Start(){
        playerControl = game.GetComponent<FirstPersonControllerCustom>();
    }

    // Update is called once per frame
    private void Update() 
    {
        //Debug.Log(transform.childCount);
        if(Input.GetMouseButtonDown(0) && transform.childCount > 1){ //when player click left mouse, the dialogue will display
            if(playerControl.dialogue == true){
                transform.GetChild(index).gameObject.SetActive(true); // display the dialogue base on the index, and add 1 to the index each loop
                index += 1;
                if (transform.childCount == index){ // if dialogue is finish, reset the index
                    index = 2;
                    playerControl.dialogue = false;
                    for (int i = 0; i <= transform.childCount - 1; i++){
                        if(transform.GetChild(i).gameObject.name.Contains("Clone")){ // all the dialogue that was create is a clone, so destroy the object after the dialogue is done
                            Destroy(transform.GetChild(i).gameObject);
                        }
                    }
                }
            } else {
                gameObject.SetActive(false); // turn off dialogue background when finish talking
            }
        }
        
    }
}
