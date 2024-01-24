using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerCamera : MonoBehaviour
{
    [SerializeField] public Camera playerCam;
    public GameObject Player;
    public float sensitivity = 2.0f;
    public float rotationX;
    public float rotationY;
    public float restrictX1;
     float restrictX2;
     float restrictChange1;
     float restrictChange2;
   

    void Start(){
       
    }

    // Update is called once per frame
    void Update()
   
    {
        // restrict player to rotate their camera
        restrictX1 = rotationX+75f;
        restrictX2 = rotationX+105f;
        if(Player.activeInHierarchy){ // get player inital rotation angle
        rotationX = playerCam.transform.eulerAngles.x;
        rotationY = playerCam.transform.eulerAngles.y;
        }



        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        //rotationY = Mathf.Clamp(rotationY, restrictX1, restrictX2); // Clamp the horizontal rotation

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        
        if(rotationX > 290 && rotationX <360){ // rotate player camera to the specific rotation
            rotationX = rotationX + 50.0f*Time.deltaTime;
        }else if(rotationX > 0 && rotationX <70){
            rotationX = rotationX - 50.0f*Time.deltaTime;
        }
        
        if(rotationY > 90){
            rotationY = rotationY - 135.0f*Time.deltaTime;
        }
        if(rotationY < 90){
            rotationY = rotationY + 135.0f*Time.deltaTime;
        }
    }
}