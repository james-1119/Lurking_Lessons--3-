using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LimitedVisionEffect : MonoBehaviour
{
    public Image visionBlocker;
    public CanvasGroup canvasGroup;


    // Start is called before the first frame update
    void Start()
    {
        // Set the initial alpha based on the hiding state
        SetHidingState(false);
    }

    public void SetHidingState(bool isHiding)
    {

        if(isHiding){
            canvasGroup.alpha = 1;
        } else {
            canvasGroup.alpha = 0;
        }
        
    }
}
