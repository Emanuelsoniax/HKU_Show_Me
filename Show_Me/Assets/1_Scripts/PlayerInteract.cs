using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public bool isHoldingItem;
    public GameObject heldItem;
    public Transform heldItemTransform;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if(heldItem == null)
            {
                return;
            }
            heldItem.GetComponent<Plank>().DropPlank(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Boulder>())
        {
            other.GetComponent<Boulder>().Interact(this.gameObject);
        }
        if (other.GetComponent<Plank>())
        {
            other.GetComponent<Plank>().Interact(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Boulder>())
        {
            other.GetComponent<Boulder>().StopInteracting(this.gameObject);
        }
    }
}

public interface IInteractable
{
    public void Interact(GameObject thing);
    public void StopInteracting(GameObject thing);
}