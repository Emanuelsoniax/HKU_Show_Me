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
    public bool plank;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        GetInteractingCount();
        if (plank == true)
        {
            state = BoulderState.Held;
        }
    }

    void GetInteractingCount()
    {
        if (playersInteracting.Count >= 1)
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
            case BoulderState.Held: HeldBehaviour(); break;
            case BoulderState.Rolling: RollingBehaviour(); break;
            case BoulderState.Pushed: PushedBehavior(); break;
        }
    }

    void HeldBehaviour()
    {
        rb.isKinematic = true;
    }

    void RollingBehaviour()
    {
        rb.isKinematic = false;
        if (axis == RollingAxis.x)
        {
            rb.AddForce(new Vector3(-rollingspeed, 0, 0));
            if (rb.velocity.x >= -maxVelocity)
            {
                rb.velocity = new Vector3(-maxVelocity, -maxVelocity, -maxVelocity);
            }
        }
        if (axis == RollingAxis.z)
        {
            rb.AddForce(new Vector3(0, 0, -rollingspeed));
            if (rb.velocity.z <= -maxVelocity)
            {
                rb.velocity = new Vector3(-maxVelocity, -maxVelocity, -maxVelocity);
            }
        }
    }

    void PushedBehavior()
    {
        rb.isKinematic = false;
        rb.AddForce(GetRollingDirection());
    }

    Vector3 GetRollingDirection()
    {
        Vector3 playerOneDir = new Vector3();
        Vector3 playerTwoDir = new Vector3();
        for (int i = 0; i < playersInteracting.Count; i++)
        {
            if (playersInteracting[i].transform.position.z <= transform.position.z)
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
        Vector3 rollingDirection = new Vector3(playerOneDir.x + playerTwoDir.x, 0, playerOneDir.z + playerTwoDir.z).normalized;
        return rollingDirection;
    }

    public void Interact(GameObject thing)
    {
        Debug.Log("interacting");

        if (thing.GetComponent<PlayerMovement>())
        {
            playersInteracting.Add(thing.GetComponent<PlayerMovement>());
        }
    }
    public void StopInteracting(GameObject thing)
    {
        Debug.Log("stopped interacting");

        if (thing.GetComponent<PlayerMovement>())
        {
            playersInteracting.Remove(thing.GetComponent<PlayerMovement>());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Plank>())
        {
            other.GetComponent<Plank>().transform.position = new Vector3(transform.position.x, other.GetComponent<Plank>().transform.position.y, transform.position.z - 1.5f);
            plank = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Plank>())
        {
            plank = false;
        }
    }
}
