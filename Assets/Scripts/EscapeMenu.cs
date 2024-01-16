using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public void backToMain(){
       
        SceneManager.LoadScene("Starting Screen"); // Load back to the starting scene
    }
}
