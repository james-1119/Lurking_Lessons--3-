using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] GameObject KeyModel;
    public GameObject game; 
    private Inventory keyPicked;
    float offTimer = 0;
    float onTimer = 0;
    public bool startTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        keyPicked = game.GetComponent<Inventory>(); // calling the inventory class so you can access the variable in it
    }

    // Update is called once per frame
    void Update()
    {
        if (keyPicked.keySelected == false){ // if not the player can't do anything with the key1
                offTimer += Time.deltaTime;

            onTimer = 0;
            if(offTimer > 0.5){
                KeyModel.gameObject.SetActive(false);
                startTimer = false;
            }
        } else {
            offTimer = 0;
            onTimer += Time.deltaTime;  
            if(onTimer > 0.5){
                KeyModel.gameObject.SetActive(true); // key1 model turn on if player selectes it                
            }
        }
    }

}
