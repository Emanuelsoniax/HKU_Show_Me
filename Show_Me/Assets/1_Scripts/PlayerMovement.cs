using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float sidewaysInput;
    float forwardInput;
    [HideInInspector]public Vector3 move;

    [SerializeField] float playerSpeed;
    CharacterController controller;

    [Header ("GroundCheck")]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    enum Player { One, Two}
    [SerializeField] Player player;

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
        
        move = transform.right * sidewaysInput + transform.forward * forwardInput;      //beweging op basis van de orientatie van de speler
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    public void GetInput()
    {
        switch (player)
        {
            case Player.One:
                sidewaysInput = Input.GetAxis("Horizontal");      //input links/rechts
                forwardInput = Input.GetAxis("Vertical");          //input voor/achter
                break;
            case Player.Two:
                sidewaysInput = Input.GetAxis("Horizontal2");      //input links/rechts
                forwardInput = Input.GetAxis("Vertical2");          //input voor/achter
                break;
        }
    }
}
