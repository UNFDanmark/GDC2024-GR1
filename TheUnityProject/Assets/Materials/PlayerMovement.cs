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
    public float permaspeed = 0f;
    public float fastspeed = -10f;
    public float slidepenalty = -5f;
    public CharacterController PlayerHeight;
    public float normalHeigth, crouchHeight;
    public float t = 0.0f;
    public float slideAccelerationTime = 0.1f;
    public float slideDeccelerationTime = 0.0001f;
    private bool isSliding;
    public float y = 0.0f;
    public bool topslidespeed;
    public float cooldown = 0.5f;
    public float cooldownleft;
    public float slideDecelerationTime = 2f;
    private bool isDecelerating;
    
    
    
    
    void Start()
    {
        currentSpeed=permaspeed;
        cooldownleft = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        //WASD
        float Z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * currentSpeed + transform.forward * Z * SideSpeed;
      //  move.x = transform.forward * baseSpeed;
       // move.z = transform.right * Z * SideSpeed;

        controller.Move(move * Time.deltaTime);
        // Space
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            
        }

        if (Input.GetButtonDown("Jump") && isGrounded && isSliding == false)
        {
            velocity.y = Mathf.Sqrt(JumpHight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime );
        
        //Crouch C
        if (Input.GetKeyDown(KeyCode.C))
        {
            isSliding = true;
            PlayerHeight.height = crouchHeight;
            currentSpeed = Mathf.Lerp(permaspeed, fastspeed, t);
            
            
            cooldownleft = cooldownleft - Time.deltaTime;
            
            print(currentSpeed);
        }
        //t += slideAccelerationTime * Time.deltaTime;

        
        if (isSliding && !isDecelerating)
        {
            t += slideAccelerationTime * Time.deltaTime;

            if (t >= 1f) // Assuming t reaches 1 when the max speed is hit
            {
                t = 1f; // Clamp t to 1
                StartCoroutine(Decelerate());
                isDecelerating = true;
            }
        }
        
        if (currentSpeed <= fastspeed)
        {
            topslidespeed = true;
        }

        if (isSliding && topslidespeed && cooldownleft <= 0)
        {
            //currentSpeed = Mathf.Lerp(fastspeed, slidepenalty, y);
            
        }
        
        //y += slideDeccelerationTime * Time.deltaTime;
        if (currentSpeed >= slidepenalty)
        {
            topslidespeed = false;
        }
        
        
        if (Input.GetKeyUp(KeyCode.C))
        {
            PlayerHeight.height = normalHeigth;
            //rejs op og normal speed
            isSliding = false;
        }
        
    }
    
    
    private IEnumerator Decelerate()
    {
        float initialSpeed = currentSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < slideDecelerationTime)
        {
            currentSpeed = Mathf.Lerp(initialSpeed, slidepenalty, elapsedTime / slideDecelerationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentSpeed = slidepenalty;
        isSliding = false;
        isDecelerating = false;
        t = 0f; // Reset t for the next slide
    }
    
}

