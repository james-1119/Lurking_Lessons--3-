using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemyAIPatrol : MonoBehaviour
{

    float timer = 0;
    GameObject player;
    NavMeshAgent agent;
    public GameObject dialogue;
    [SerializeField] trigger Trigger;
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] float chaseSpeed = 5f;
    [SerializeField] float walkSpeed = 2f;

    // Enemy patrolling
    Vector3 destPoint;
    bool walkPointSet;
    bool start;
    BoxCollider enemyCollider;
    [SerializeField] float walkRange;

    public bool playerInHidingPlace; // Variable to track if the player is in a hiding place

    // state changing variables
    [SerializeField] float sightRange, attackRange;
    bool playerInSight, playerInAttackRange;
    bool chasingPlayer;
    // Start is called before the first frame update
    void Start()
    {    

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        Trigger = Trigger.GetComponent<trigger>();
        
        enemyCollider = GetComponent<BoxCollider>();
        if (Trigger == null)
        {
            Debug.LogError("Trigger component not found. Make sure the object has a 'trigger' component.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        start = Trigger.chaseInitialize; // Trigger for the enemy to start chasing player
        if(start == true){
            dialogue.SetActive(false);

            Vector3 toPlayer = player.transform.position - transform.position; // enemy going toward player position
            float angleToPlayer = Vector3.Angle(transform.forward, toPlayer);
            playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer) && angleToPlayer < 120f; // check if player is in sight

            if (!playerInSight){ // if the player is not in sight it search for player
                Patrol();
            } 
            if (playerInSight && !playerInHidingPlace){ // if player is in sight and is not in hiding place, start chasing player
                chasingPlayer = true;
            }
            if(playerInHidingPlace){ // enemy stop chasing player after player enters hide place
                Patrol();
                chasingPlayer = false;
            }

            if (chasingPlayer ==  true){
                Chase();
            } else {
                StopChase();
            }
        }

        //Debug.Log(playerInSight);
    }

    void Chase(){

        agent.speed = chaseSpeed;
        agent.acceleration = 100; // Set acceleration to a high value
        agent.stoppingDistance = 0.1f; // Set stopping distance to a small value
        agent.SetDestination(player.transform.position);
        //print("enemy spotted you");
    }
    void ResetSpeed()
    {
        agent.speed = walkSpeed; // assuming you have the original speed stored somewhere
    }

    void Patrol(){
        timer += Time.deltaTime;
        if(!walkPointSet) { // enemy randomly searching for a destination
            timer = 0;
            SearchForDest();
        }
        if(timer > 10){ // every 10 second enemy changes it's search direction
            timer = 0;
            SearchForDest();
        }
        if(walkPointSet) { // check if the destination is reachable
            agent.SetDestination(destPoint);
        }
        if(Vector3.Distance(transform.position, destPoint) < 30){
            walkPointSet = false;
        } 
        //print("enemy searching for you");
    }
    void StopChase()
    {
        playerInSight = false; // Stop chasing
        ResetSpeed();
        //Patrol();
        //print("Player is in hiding place. Stopping chase.");
    }

    void SearchForDest(){
        float posz = Random.Range(-walkRange,walkRange);// select a randome x and y coordinate
        float posx = Random.Range(-walkRange,walkRange);

        destPoint = new Vector3(transform.position.x + posx, transform.position.y, transform.position.z + posz);

        if(Physics.Raycast(destPoint, Vector3.down, groundLayer)){ // check if the destination is reachable
            walkPointSet = true;
        }
    }

    private void OnTriggerEnter(Collider other){
        var player = other.GetComponent<FirstPersonControllerCustom>();

        if (player != null && start == true){ // if the player collided with the enemy while escaping, switch scene to end page
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("EndScreen");
        }
        
    }
    
}