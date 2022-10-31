using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEffect : MonoBehaviour
{
    [SerializeField] Color effectColor;
    [SerializeField] float effectHeight;
    [SerializeField] Transform target;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = effectColor;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(target.position.x, effectHeight, target.position.z);
    }
}
