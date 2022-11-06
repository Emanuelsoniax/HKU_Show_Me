using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Port : MonoBehaviour
{
    public Animator portAnimator;
    public Transform gateTransform;
    float gateYPos;
    public float animSpeed;

    public float maxSimultaneous; // The max time between p1 interact and p2 interact to still count as a simultaneous input

    public Gear[] gears;
    // Update is called once per frame

    private void Start()
    {
        gateYPos = 0.3f;
    }

    void Update()
    {
        Debug.Log(gateYPos);

        if (CheckGears()) // If both players are interacting
        {
            //portAnimator.SetTrigger("Open");
            gateYPos+= 0.1f * animSpeed;
            gateTransform.position = GetGatePosition();
            if (gateYPos >= 10)
            {
                gateYPos = 10;
            }
        }

        if (!CheckGears()) // one of the players walks away from the gears
        {
            //portAnimator.SetTrigger("Close");
            gateYPos-= 0.1f * animSpeed;
            gateTransform.position = GetGatePosition();
            if (gateYPos <= 0.3f)
            {
                gateYPos = 0.3f;
            }
        }
    }

    Vector3 GetGatePosition()
    {
        Vector3 gatePos = new Vector3(gateTransform.position.x, gateYPos + 4.6f, gateTransform.position.z);
        return gatePos;
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
