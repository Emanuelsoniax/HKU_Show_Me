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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
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
            if (rb.velocity.y >= maxVelocity)
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

    }

    public void Interact(PlayerInteract player)
    {
        Debug.Log("interacting");

        if (state == BoulderState.Rolling)
        {
            state = BoulderState.Held;
        }

        //if(state == BoulderState.Held)
        //{
        //    state = BoulderState.Pushed;
        //}
    }
    public void StopInteracting(PlayerInteract player)
    {
        if (state == BoulderState.Held)
        {
            state = BoulderState.Rolling;
        }

        //if (state == BoulderState.Pushed)
        //{
        //    state = BoulderState.Held;
        //}
    }
}
