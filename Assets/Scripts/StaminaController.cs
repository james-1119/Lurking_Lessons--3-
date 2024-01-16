using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaController : MonoBehaviour
{
    // initialize all the variables
    public float playerStamina = 100.0f; // player initial stamina 
    public float maxStamina = 100.0f; // maximum stamina 
    public bool hasRegenerated = true;
    public bool weAreSprinting = false;

    public int walkSpeed = 4;
    public int runSpeed = 7;
    public float staminaDrain = 0.5f;
    public float staminaRegen = 0.5f;

    public Image staminaProgressUI = null;
    public CanvasGroup sliderCanvasGroup = null;

    private FirstPersonControllerCustom playerController; // call first player controller calss

    private void Start(){ 
        playerController = GetComponent<FirstPersonControllerCustom>();
    }
    
    private void Update(){
        if (!weAreSprinting){
            if (playerStamina <= maxStamina - 0.01){ // player regenerate stamina if they don't run
                playerStamina += staminaRegen * Time.deltaTime;
                UpdateStamina(1);

                if (playerStamina >= maxStamina){ // turn off stamina bar image if the stamina is max

                    sliderCanvasGroup.alpha = 0;
                    hasRegenerated = true;
                }
            }
        }
    }

    public void Sprinting(){
        if(hasRegenerated){//every time player runs the stamina decress
            weAreSprinting = true;
            playerStamina -= staminaDrain*Time.deltaTime;
            UpdateStamina(1);

            if (playerStamina <= 0){// turn off stamina bar image if the stamina is 0
                hasRegenerated = false;
                
                sliderCanvasGroup.alpha = 0;
            }
        }
    }
    void UpdateStamina(int value){ //update the stamina bar image
        staminaProgressUI.fillAmount = playerStamina/maxStamina; // stamina bar image ratio

        if (value == 0){
            sliderCanvasGroup.alpha = 0;
        } else {
            sliderCanvasGroup.alpha = 1;
        }
    }

}
