using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Port : MonoBehaviour
{
    public Animator portAnimator;

    public float maxSimultaneous; // The max time between p1 interact and p2 interact to still count as a simultaneous input
    //float simultaneousTimer;

    //bool ePress;
    //bool pPress;


    public Gear[] gears;
    //public Gear gear1;
    //public Gear gear2;

    // Update is called once per frame
    void Update()
    {

        //simultaneousTimer -= Time.deltaTime;

        //if (gear1.occupied && gear2.occupied) // If both gears are occupied, check for input
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        simultaneousTimer = maxSimultaneous;
        //        ePress = true;
        //    }
                
        //    if (Input.GetKeyDown(KeyCode.P))
        //    {
        //        simultaneousTimer = maxSimultaneous;
        //        pPress = true;
        //    }
        //}

        //if (simultaneousTimer < 0) // If the keys were not pressed simultaneously, reset
        //{
        //    ePress = false;
        //    pPress = false;
        //}

        if (CheckGears()) // If both players are interacting
        {
            portAnimator.SetTrigger("Open");
        }

        if (!CheckGears()) // one of the players walks away from the gears
        {
            portAnimator.SetTrigger("Close");
        }

    }

    bool CheckGears()
    {
        int gearCounter = 0;

        for (int i = 0; i < gears.Length; i++)
        {
            if (gears[i].playerIsInteracting)
            {
                gearCounter++;
            }
        }

        if (gearCounter == 2)
        {
            return true;
        }
        else { return false; }
    }
}
