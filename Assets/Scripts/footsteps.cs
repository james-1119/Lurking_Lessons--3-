using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class footsteps : MonoBehaviour
{
    public GameObject step;
    // Start is called before the first frame update
    void Start()
    {
        step.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("w"))
        {
            footstep();
        }

        if(Input.GetKeyDown("s"))
        {
            footstep();
        }

        if(Input.GetKeyDown("a"))
        {
            footstep();
        }

        if(Input.GetKeyDown("d"))
        {
            footstep();
        }

        if(Input.GetKeyUp("w"))
        {
            StopFootstep();
        }

        if(Input.GetKeyUp("s"))
        {
            StopFootstep();
        }

        if(Input.GetKeyUp("a"))
        {
            StopFootstep();
        }

        if(Input.GetKeyUp("d"))
        {
            StopFootstep();
        }

    }

    void footstep()
    {
        step.SetActive(true);
    }

    void StopFootstep()
    {
        step.SetActive(false);
    }
}
