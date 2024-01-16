using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerCamera : MonoBehaviour
{
    public float sensitivity = 2.0f;
    private float rotationX = 90f;
    private float rotationY = 0.0f;
    private float restrictX1;
    private float restrictX2;
    

    void Start(){
        restrictX1 = rotationX-15f;
        restrictX2 = rotationX+15f;
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, restrictX1, restrictX2); // Clamp the horizontal rotation

        transform.localRotation = Quaternion.Euler(0, rotationY, 0);
    }
}
