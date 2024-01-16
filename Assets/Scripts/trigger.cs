using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    [SerializeField] Animator myAnimationController;
    public bool chaseInitialize;

    // Start is called before the first frame update
    void Start()
    {
        chaseInitialize = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        chaseInitialize = true;
        myAnimationController.SetTrigger("Running");
    }
}
