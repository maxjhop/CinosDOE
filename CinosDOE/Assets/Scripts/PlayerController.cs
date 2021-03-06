using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float MovementSpeed = 10f;
    public float Gravity = 18f;
    public float jumpHeight = 10f;
    //distance from the middle of the collider to the ground
    public float distToGround = 1f;
    private bool flyTextSelected = false;
    public GameObject Cooldowns;
    public Transform groundCheck;
    public LayerMask groundMask;
    Vector3 velocity;
    public bool isGrounded;
    //private bool isFalling;
    public AudioSource wings;
    public AudioSource thud;
    public float FlyHeight = 20f;
    private bool fly = false;
    private bool flyPeak = false;
    private float flyStart;
    private float descendCooldown = 2f;
    private float flyCooldownStart = 0f;
    private float flyCooldown = 5f;
    private float buttonHeld = 0f;
    private Text flyText;
    //private float jumpStart = 0;



    void Start()
    {
        //wings = GetComponent<AudioSource>();
    }

    void FlyAbility() 
    {
        if (Input.GetButtonDown("Fly") && flyCooldownStart <= 0)
        {
            flyStart = groundCheck.position.y;
            velocity.y = MovementSpeed * 5;
            characterController.Move(velocity * Time.deltaTime);
            fly = true;
            wings.Play();
            //Cooldowns.text = flyCooldownStart.ToString();


        }
        if (fly)
        {
            Debug.Log(fly);
            if ((groundCheck.position.y - flyStart) > FlyHeight)
            {
                flyPeak = true;
            }
            if (flyPeak) 
            { 
                velocity.y = 0f;
                characterController.Move(velocity * Time.deltaTime);
                descendCooldown -= Time.deltaTime;
                Gravity = 0f;
                if (descendCooldown <= 0)
                {
                    Gravity = 18f;
                    fly = false;
                    descendCooldown = 2f;
                    flyPeak = false;
                    flyCooldownStart = flyCooldown;

                }

            }

        }
        if (flyCooldownStart > 0)
        {
            if (!flyTextSelected)
            {
                foreach (Transform child in Cooldowns.transform)
                {
                    Text Text = child.GetComponent<Text>();
                    if (Text.text == "")
                    {
                        flyText = child.GetComponent<Text>();
                        flyTextSelected = true;
                        break;
                    }
                }
            }
            flyText.text = "Fly burst cooldown: " + Math.Round(flyCooldownStart, 2).ToString();
            flyCooldownStart -= Time.deltaTime;
            if (flyCooldownStart <= 0 && fly == false)
            {
                flyText.text = "";
                flyTextSelected = false;
            }

        }
        
        

    }

    
    void Update()
    {
        //TimerScript.Instance.UpdateTime();
        
        isGrounded = Physics.CheckSphere(groundCheck.position, distToGround, groundMask);
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            buttonHeld = Time.time + .5f;
            Debug.Log("Jump");
            velocity.y = jumpHeight;
            characterController.Move(velocity * Time.deltaTime);
        }

        if (Input.GetButton("Jump") && !isGrounded && velocity.y < 0)
        {
            if (Time.time >= buttonHeld)
            {
                Gravity = 4.4f; 
            }
        }
        else
        {
            Gravity = 18f;
        }

        if (Input.GetButton("Sprint") && !fly && MovementSpeed != 0)
        {
            MovementSpeed = 18f;
        }
        else if (MovementSpeed != 0)
        {
            MovementSpeed = 10f;
        }

        // Gravity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
            
        }
        
        velocity.y -= Gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        FlyAbility();
        
    }
    /*
    void OnCollisionEnter (Collision col)
    {
        Debug.Log("Collided with nextLevelPad!");
        if (col.collider.tag == "NextLevel")
        {
            //Debug.Log("Collided with nextLevelPad!");
            SceneManager.LoadScene("LevelTwo");
        }
    }
    */
    /*
    void onTriggerEnter(Collider other)
    {
        if (other.transform.tag == "NextLevel")
        {
            SceneManager.LoadScene("LevelTwo");
        }
    }
    */
}