using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public bool occupied;
    //public Port port;
    public bool playerIsInteracting;
    public Transform gearTransform;

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && occupied == true)
        {
            playerIsInteracting = true;
        }
        else
        {
            playerIsInteracting = false;
        }

        if (playerIsInteracting)
        {
            gearTransform.Rotate(1,0,0);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            occupied = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            occupied = false;
        }
            
    }
}
