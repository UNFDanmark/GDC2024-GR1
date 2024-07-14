using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    public float gravity = -9.82f;
    private Vector3 velocity;
    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    public float JumpHight = 3f;
    public float SideSpeed = 5f;
    public float currentSpeed = 0f;
    public float permaspeed = -10f;
    public float fastspeed = -25f;
    public float slidepenalty = -5f;
    public CharacterController PlayerHeight;
    public float normalHeigth, crouchHeight;
    public float slideaccelerationTime = 0.1f;
    public float cooldown = 0.5f;
    public float slidedecelerationTime = 2f;
    private bool isSliding = false;
    private bool isDecelerating = false;
    private float slideTime = 0f;
    private float decelTime = 0f;
    
    void Start()
    {
        currentSpeed = permaspeed;
    }
    
    void Update()
    {
        //WASD
        float Z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * currentSpeed + transform.forward * Z * SideSpeed;

        controller.Move(move * Time.deltaTime);
        // Isgrounded (transformen i unity på karakterens fod)
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            
        }
        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded && !isSliding)
        {
            velocity.y = Mathf.Sqrt(JumpHight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime );
        
        // Slide
        if (Input.GetKeyDown(KeyCode.C))
        {
            isSliding = true;
            isDecelerating = false;
            slideTime = 0f;
            decelTime = 0f;
            PlayerHeight.height = crouchHeight;
        }

        if (isSliding)
        {
            if (slideTime < cooldown)
            {
                // Accelerate to fastspeed
                currentSpeed = Mathf.Lerp(permaspeed, fastspeed, slideTime / slideaccelerationTime);
            }
            else
            {
                // Decelerate to slidepenalty
                isDecelerating = true;
            }

            if (isDecelerating)
            {
                currentSpeed = Mathf.Lerp(fastspeed, slidepenalty, decelTime / slidedecelerationTime);
                decelTime += Time.deltaTime;
            }

            slideTime += Time.deltaTime;
        }
        
        
        if (Input.GetKeyUp(KeyCode.C))
        {
            PlayerHeight.height = normalHeigth;
            isSliding = false;
            currentSpeed = permaspeed;
        }
        
    }
    
}

