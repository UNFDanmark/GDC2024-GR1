using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    ////////////////////////////////////////////////// Movement //////////////////////////////////////////////////////
    public float gravity = -9.82f;
    private Vector3 velocity;
    public Transform GroundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    public float JumpHight = 5f;
    public float SideSpeed = 5f;
    public float currentSpeed = 0f;
    public float permaspeed = -10f;
    public float fastspeed = -20f;
    public float slidepenalty = -5f;
    public Transform PlayerHeight;
    public float normalHeigth, crouchHeight;
    public float slideaccelerationTime = 0.1f;
    public float cooldown = 0.5f;
    public float slidedecelerationTime = 2f;
    private bool isSliding = false;
    private bool isDecelerating = false;
    private float slideTime = 0f;
    private float decelTime = 0f;
    public GameObject foothitbox;
    public Collider PlayerBody;
    private bool Isjumping = false;
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    ////////////////////////////////////////////////// health/game over //////////////////////////////////////////////
    public GameObject gameOverScreen;
    public float MaxHealth = 5f;
    public float currenthealth;
    public float attackedCooldown = 0.5f;
    public float currentattackedCooldown;
    public bool hasbeenattacked = false;
    public GameObject UI;
    
    
    ///////////////////////////////////////////////////// score //////////////////////////////////////////////////
    
    public TextMeshProUGUI timer;
    public TextMeshProUGUI HighScore;
    public TextMeshProUGUI MyScore;
    private float Currentscore;

    public float StartMult = 1f;
    public float SlutMult = 10f;
    public float currentMult;
    public float Multvariable = 0f;
    public bool Mu = true;
    
    ///////////////////////////////////////////////////// animation //////////////////////////////////////////////////
    
    public Animator animator;

    ////////////////////////////////////////////////// Audio //////////////////////////////////////////////////////
    AudioSource audioSource;
    public AudioClip music;
    public AudioClip jumpsound;
    public AudioClip runningsound;
    public AudioClip slidingsound;
    private bool justLanded = false;
  

    void Start()
    {
        currentSpeed = permaspeed;
        currenthealth = MaxHealth ;
        currentattackedCooldown = attackedCooldown;
        currentMult = StartMult;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1f;
      
    }
    
    void Update()
    {

       
        ////////////////////////////////////////////////// Movement //////////////////////////////////////////////////

        //WASD
        float Z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * currentSpeed * currentMult + transform.forward * Z * SideSpeed;

        if (Mu)
        {
            if (currentMult < SlutMult)
            {
                currentMult = Mathf.Lerp(StartMult, SlutMult, (0.1f*Mathf.Pow(1.015f,Multvariable)) / 95.34f);
            
            
            }

            Multvariable += Time.deltaTime;


        }
        
        
        controller.Move(move * Time.deltaTime);
        // Isgrounded (transformen i unity på karakterens fod)
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("Isjumping", false);
            Isjumping = false;
            
            if(justLanded)
            {
                audioSource.Stop();
                justLanded = false;
            }
            if (!audioSource.isPlaying)
            {
                audioSource.volume = 1f;
                print("Run sound");
            
                audioSource.Play();
              
            } 

            
        }
        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded && !isSliding)
        {
            Isjumping = true;
            //velocity = new Vector3(move.x * -1f, Mathf.Sqrt(JumpHight * -2f * gravity), 0);
            velocity.y = Mathf.Sqrt(JumpHight * -2f * gravity);
            animator.SetBool("Isjumping", true);
            audioSource.Stop();
            audioSource.volume = 1f;
            audioSource.PlayOneShot(jumpsound);
            justLanded = true;
            print("Air");
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime );
        
        // Slide
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Isjumping)
        {
            isSliding = true;
            isDecelerating = false;
            slideTime = 0f;
            decelTime = 0f;
            PlayerHeight.position = new Vector3(PlayerHeight.position.x,crouchHeight,PlayerHeight.position.z);
            PlayerBody.enabled = false;
            animator.SetBool("Issliding", true);
            audioSource.volume = 0.05f;
            audioSource.PlayOneShot(slidingsound);
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
        
        
        if (Input.GetKeyUp(KeyCode.LeftShift) && !Isjumping)
        {
            PlayerHeight.position = new Vector3(PlayerHeight.position.x,normalHeigth,PlayerHeight.position.z);
            isSliding = false;
            currentSpeed = permaspeed;
            
            PlayerBody.enabled = true;
            animator.SetBool("Issliding", false);
            audioSource.Stop();

        }
        
        ///////////////////////////////////////////////////// score //////////////////////////////////////////////////
        
        
        
        
        /////////////////////////////////////////////// health/game over ///////////////////////////////////////////
        if (transform.position.y < -25)
        {
            currenthealth = 0;
        }
        
        
        if (currenthealth <= 0f)
        {
            bool sm = ScoreManager.instance.UpdateHighScore(Currentscore);
            MyScore.text = "My Score: " + Currentscore;
            if (sm)
            {
                HighScore.text = "High Score: " + Currentscore;
            }
            else
            {
                HighScore.text = "High Score: " + ScoreManager.instance.CurrentHighScore;
            }

            UI.SetActive(false);
            gameOverScreen.SetActive(true);
            
        }
        else
        {
            Currentscore = Mathf.Floor(Time.timeSinceLevelLoad*3) ;
            timer.text = "" + Currentscore;
        }

        if (currentattackedCooldown > -1f)
        {
            currentattackedCooldown -= Time.deltaTime;
        }
        
        
        
    }
    

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        print("controller hit");
        if (hit.gameObject.CompareTag("Enemy") || hit.gameObject.CompareTag("Obstacle"))
        {
            print("av2");
            if (currentattackedCooldown <= 0f)
            {
                currenthealth--;
                currentattackedCooldown = attackedCooldown;
                hasbeenattacked = true;
            }
            
            
        } 
    }

}

