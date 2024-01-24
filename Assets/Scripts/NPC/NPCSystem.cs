using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCSystem : MonoBehaviour
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
        if(player_detection == true && Input.GetKeyDown(KeyCode.R) && !playerControl.dialogue){ // while player is not in dialogue, they can press r to talk
            canva.SetActive(true);
            playerControl.dialogue = true;
            NewDialogue("Hello my friend"); // these are the dialogues to display
            NewDialogue("You are asking about the key huh?");
            NewDialogue("Pretty sure I saw a green one on the other end of the hallway");
            NewDialogue("");
            canva.transform.GetChild(1).gameObject.SetActive(true); // turn on the dialogue background
        }
    }

    void NewDialogue(string text){
        GameObject template_Clone = Instantiate(dialogue, dialogue.transform); // make a clone of the dialogue template, and add the text into it
        template_Clone.transform.parent = canva.transform;
        template_Clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;

    }

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            player_detection = true;
            intText.SetText("Press [R] to talk to Will"); // display text to tell player they can talk to npc
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
