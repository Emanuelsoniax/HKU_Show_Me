using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Boulder>().Interact(this);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Boulder>().StopInteracting(this);
    }
}

public interface IInteractable
{
    public void Interact(PlayerInteract player);
    public void StopInteracting(PlayerInteract player);
}