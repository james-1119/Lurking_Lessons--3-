using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void EndGame(){
        Debug.Log("Hi");
        SceneManager.LoadScene("MainScene"); //Load Scene to make player restart
    }
}
