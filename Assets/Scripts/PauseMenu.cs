using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Camera PlayerCam;
    float x;
    float y;
    float z;
    public bool onPause = false;
    public void Pause()
    {
        x = PlayerCam.transform.eulerAngles.x; // record player camera rotation
        y = PlayerCam.transform.eulerAngles.y;
        z = PlayerCam.transform.eulerAngles.z;
        Cursor.lockState = CursorLockMode.None; // unlock the mouse
        pauseMenu.SetActive(true); // pause menu canva display
        Time.timeScale = 0f; // pause the game
        onPause = true;
    }

    public void Resume(){
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor again
        pauseMenu.SetActive(false); // turn off pause menu
        Time.timeScale = 1f; // let game continue to run
        onPause = false;
    }

    public void Home(){
        Cursor.lockState = CursorLockMode.None; // unlock the mouse
        Cursor.visible = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // load the starting scene
        onPause = false;
    }

    public void LockCamera(){
        PlayerCam.transform.rotation = Quaternion.Euler(x,y,z); // lock player camera rotation
    }

    void Update(){
        if(onPause == true){ // when player is on pause, lock the camera
            LockCamera();
        }
    }
}
