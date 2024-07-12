using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10f;
    private Rigidbody rb;

    public float moveSpeed = 3f;
    //private CharacterController controller;
    //private Vector3 playerVelocity;
    //private bool groundedPlayer;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = rb.velocity;
       //ovement.z = Input.GetAxisRaw("Horizontal") * speed;
        movement.x = Input.GetAxisRaw("Vertical") * speed;
        rb.velocity = movement;
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        





    }
}

