using System;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour, IInteractable
{
    Rigidbody rb;
    public enum BoulderState { Rolling, Held, Pushed }
    public BoulderState state;

    [Header("Rolling Settings")]
    [SerializeField] float rollingspeed;
    [SerializeField] float maxVelocity;
    enum RollingAxis { x, z }
    [SerializeField] RollingAxis axis;

    List<PlayerMovement> playersInteracting = new List<PlayerMovement>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInteractingCount();
        CheckState();
    }

    void GetInteractingCount()
    {
        if (playersInteracting.Count == 1)
        {
            state = BoulderState.Held;
        }
        if (playersInteracting.Count == 2)
        {
            state = BoulderState.Pushed;
        }
        if (playersInteracting.Count == 0)
        {
            state = BoulderState.Rolling;
        }
    }

    void CheckState()
    {
        switch (state)
        {
            case BoulderState.Rolling: RollingBehaviour(); break;
            case BoulderState.Held: HeldBehaviour(); break;
            case BoulderState.Pushed: PushedBehavior(); break;
        }
    }

    void RollingBehaviour()
    {
        if (axis == RollingAxis.x)
        {
            rb.AddForce(new Vector3(rollingspeed, 0, 0));
            if (rb.velocity.x >= maxVelocity)
            {
                rb.velocity = new Vector3(maxVelocity,0,0);
            }
        }
        if (axis == RollingAxis.z)
        {
            rb.AddForce(new Vector3(0, 0, rollingspeed));
            if (rb.velocity.z >= maxVelocity)
            {
                rb.velocity = new Vector3(0, 0, maxVelocity);
            }
        }
    }

    void HeldBehaviour()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.angularVelocity = new Vector3(0, 0, 0);
    }

    void PushedBehavior()
    {
        rb.AddForce(GetRollingDirection());
    }

    Vector3 GetRollingDirection()
    {
        Vector3 playerOneDir = new Vector3();
        Vector3 playerTwoDir = new Vector3();
        for (int i=0; i <playersInteracting.Count; i++)
        {
            if(playersInteracting[i].transform.position.z <= transform.position.z)
            {
                switch (i)
                {
                    case 1: 
                        playerOneDir = playersInteracting[1].move;
                        break;
                    case 2:
                        playerTwoDir = playersInteracting[2].move;
                        break;
                }
            }
        }
        Vector3 rollingDirection = new Vector3(playerOneDir.x + playerTwoDir.x, 0, playerOneDir.z + playerTwoDir.z);
        return rollingDirection;
    }

    public void Interact(PlayerInteract player)
    {
        Debug.Log("interacting");
        playersInteracting.Add(player.GetComponent<PlayerMovement>());
    }
    public void StopInteracting(PlayerInteract player)
    {
        Debug.Log("stopped interacting");
        playersInteracting.Remove(player.GetComponent<PlayerMovement>());
    }
}
