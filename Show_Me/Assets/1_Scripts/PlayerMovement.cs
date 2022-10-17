using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float sidewaysInput;
    float forwardInput;

    [SerializeField] float playerSpeed;
    CharacterController controller;


    [Header ("GroundCheck")]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);     //groundcheck 
        
        Vector3 move = transform.right * sidewaysInput + transform.forward * forwardInput;      //beweging op basis van de orientatie van de speler
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    void GetInput()
    {
        sidewaysInput = Input.GetAxis("Horizontal");      //input links/rechts
        forwardInput = Input.GetAxis("Vertical");          //input voor/achter
    }
}
