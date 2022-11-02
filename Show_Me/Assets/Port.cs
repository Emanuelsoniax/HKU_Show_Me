using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    public float maxSimultaneous; // The max time between p1 interact and p2 interact to still count as a simultaneous input
    float simultaneousTimer;

    bool ePress;
    bool pPress;

    public Gear gear1;
    public Gear gear2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        simultaneousTimer -= Time.deltaTime;

        if (gear1.occupied && gear2.occupied) // If both gears are occupied, check for input
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                simultaneousTimer = maxSimultaneous;
                ePress = true;
            }
                
            if (Input.GetKeyDown(KeyCode.P))
            {
                simultaneousTimer = maxSimultaneous;
                pPress = true;
            }
        }

        if (simultaneousTimer < 0) // If the keys were not pressed simultaneously, reset
        {
            ePress = false;
            pPress = false;
        }

        if (ePress && pPress) // If the keys are pressed simultaneously
        {
            // play animation of port going up
            transform.position = transform.position + new Vector3(0, 1, 0);
            ePress = false;
            pPress = false;
        }

        if (!gear1.occupied || !gear2.occupied) // one of the players walks away from the gears
        {
            // play the port falling down animation
            transform.position = new Vector3(transform.position.x, 3, transform.position.z);
        }

    }
}
