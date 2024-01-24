using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCSystem2 : MonoBehaviour
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
            NewDialogue("What you looking at nerd?");
            NewDialogue("Art room key? I toss it to the washroom already.");
            NewDialogue("Now go away before I beat you up");
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
            intText.SetText("Press [R] to talk to Jame");
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
