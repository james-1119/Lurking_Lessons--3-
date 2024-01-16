using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] GameObject FlashlightLight; // calling the flash light model and flash light light
    [SerializeField] GameObject FlashlightModel;
    [SerializeField] Animator myAnimationController;

    private bool FlashlightActive = false; // check flash light active
    
    public GameObject game; 
    private Inventory FlashLightPicked;

    float offTimer = 0;
    float onTimer = 0;

    public bool startTimer = false;


    // Start is called before the first frame update
    void Start()
    {
        FlashLightPicked = game.GetComponent<Inventory>(); // calling the inventory class so you can access the variable in it
        FlashlightLight.gameObject.SetActive(false); // turn off the light of flash light at start
    }

    // Update is called once per frame
    void Update()
    {   // On F key pressed, the player can turn on and off their flashlight
        if (Input.GetKeyDown(KeyCode.F)){
            if (FlashlightActive == false && FlashLightPicked != null && FlashLightPicked.flashLightSelected == true){ // check if flash light is open, and if player has selected flash light
                //Debug.Log(FlashLightPicked.flashLightSelected);
                FlashlightLight.gameObject.SetActive(true); // flashlight light turn on
                FlashlightActive = true;
            } else {
                FlashlightLight.gameObject.SetActive(false);
                FlashlightActive = false;
            }
        }
        if (FlashLightPicked.flashLightSelected == false){ // if not the player can't do anything with the flash light
                offTimer += Time.deltaTime;

            onTimer = 0;
            FlashlightLight.gameObject.SetActive(false);
            if(offTimer > 0.5){
                FlashlightModel.gameObject.SetActive(false);
                startTimer = false;
            }
            FlashlightActive = false;
        } else {
            offTimer = 0;
            onTimer += Time.deltaTime;
            
            if(onTimer > 0.5){
                FlashlightModel.gameObject.SetActive(true); // flash light model turn on if player selectes it                
            }
        }
        //Debug.Log(FlashLightPicked.flashLightSelected);
    }
}
