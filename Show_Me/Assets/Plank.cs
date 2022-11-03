using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour, IInteractable
{
    public float breakdownTime;
    float time = 0;

    public void Interact(GameObject thing)
    {
        PickUpPlank(thing.GetComponent<PlayerInteract>());
    }

    public void StopInteracting(GameObject thing)
    {
        DropPlank(thing.GetComponent<PlayerInteract>());
    }

    public void BreakDown()
    {
        time -= Time.deltaTime;
        if(time < breakdownTime)
        {
            Destroy(gameObject);
            FindObjectOfType<Boulder>().plank = false;
        }
    }

    void PickUpPlank(PlayerInteract player)
    {

        if(player.isHoldingItem && player.heldItem.GetComponent<Plank>())
        {
            player.heldItem.GetComponent<Plank>().DropPlank(player);
        }
        player.heldItem = this.gameObject;
        this.transform.parent = player.gameObject.transform;
        transform.position = player.heldItemTransform.position;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        player.isHoldingItem = true;
        FindObjectOfType<Boulder>().plank = false;
        
        foreach(Collider collider in GetComponents<Collider>())
        {
            collider.enabled = false;
        }
    }

    public void DropPlank(PlayerInteract player)
    {
        this.transform.parent = null;
        player.heldItem = null;
        player.isHoldingItem = false;
        GetComponent<Rigidbody>().useGravity = true;

        foreach (Collider collider in GetComponents<Collider>())
        {
            collider.enabled = true;
        }
    }
}
