using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerControl : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float gravity = -9.8f;
    public float jumpHeight = 2f;
    private CharacterController playercontroler;
    private Vector3 velocity;
    private bool isGrounded;
    public Transform groundCheck;
    public float grounDistance = 0.5f;
    public LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        playercontroler = GetComponent<CharacterController>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, grounDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playercontroler.Move(moveDirection * runSpeed * Time.deltaTime);
        }
        else
        {
            playercontroler.Move(moveDirection * walkSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2f);
        }

        velocity.y += gravity * Time.deltaTime;
        playercontroler.Move(velocity * Time.deltaTime);
    }
}
