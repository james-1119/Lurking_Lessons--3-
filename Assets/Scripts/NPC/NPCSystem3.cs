using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCSystem3 : MonoBehaviour
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
            NewDialogue("Wait Kid, ");
            NewDialogue("You are trying to run away aren't you.");
            NewDialogue("Quit lying, no one goes to washroom at this time.");
            NewDialogue("Listen, I can help you.");
            NewDialogue("There is an emergency exit in the library, that's the only place you can escape.");
            NewDialogue("But you need to get the key to unlock it, I believe an art teacher has it, you can probably find it in his room.");
            NewDialogue("Why am I helping you? There is no reason.");
            NewDialogue("Now go on, before the teacher gets suspicious.");
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
            intText.SetText("Press [R] to talk to Mystery Man");
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
