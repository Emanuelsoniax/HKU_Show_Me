using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankStructure : MonoBehaviour
{
    public float health;
    public Plank[] planks;
    List<Rigidbody> planksRB = new List<Rigidbody>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(Plank plank in planks)
        {
            planksRB.Add(plank.GetComponent<Rigidbody>());
            plank.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Collapse();
        }
    }

    private void Collapse()
    {
        //foreach(Plank plank in planks)
        //{
        //    plank.enabled = true;
        //}

        //foreach(Rigidbody rb in planksRB)
        //{
        //    rb.useGravity = true;
        //    rb.isKinematic = false;
        //}

        GetComponent<Animator>().SetTrigger("Collapse");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Boulder>())
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        health--;
    }
}
