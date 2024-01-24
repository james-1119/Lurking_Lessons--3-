using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCSystem4 : MonoBehaviour
{
    public FirstPersonControllerCustom playerControl;
    public GameObject dialogue;
    public GameObject canva;
    public GameObject game;
    public TextMeshProUGUI intText;
    public bool player_detection = false;

    void Start(){
        playerControl = game.GetComponent<FirstPersonControllerCustom>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player_detection == true && Input.GetKeyDown(KeyCode.R) && !playerControl.dialogue){
            canva.SetActive(true);
            playerControl.dialogue = true;
            NewDialogue("Going to Washroom?");
            NewDialogue("Alright, come back quickly.");
            NewDialogue("");
            canva.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void NewDialogue(string text){
        GameObject template_Clone = Instantiate(dialogue, dialogue.transform);
        template_Clone.transform.parent = canva.transform;
        template_Clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;

    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            player_detection = true;
            intText.SetText("Press [R] to talk to Mr.Bin");
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Player"){
            player_detection = false;
            intText.SetText("");
            Debug.Log("Exit");
        }
    }
    
}
