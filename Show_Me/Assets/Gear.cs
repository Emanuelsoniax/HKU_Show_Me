using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public bool occupied;

    public Port port;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            occupied = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            occupied = false;
        }
            
    }
}
