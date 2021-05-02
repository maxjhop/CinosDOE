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
    public Text Cooldowns;
    public Transform groundCheck;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    public float FlyHeight = 20f;
    private bool fly = false;
    private float flyStart;
    private float descendCooldown = 2f;
    private float flyCooldownStart = 0f;
    private float flyCooldown = 5f;

    void FlyAbility() 
    {
        if (Input.GetButtonDown("Fly") && flyCooldownStart <= 0)
        {
            flyStart = groundCheck.position.y;
            flyCooldownStart = flyCooldown;
            velocity.y = MovementSpeed * 10;
            characterController.Move(velocity * Time.deltaTime);
            fly = true;
            //Cooldowns.text = flyCooldownStart.ToString();


        }
        if (fly)
        {
            Debug.Log(fly);
            if ((groundCheck.position.y - flyStart) > FlyHeight)
            {
                velocity.y = 0f;
                characterController.Move(velocity * Time.deltaTime);
                descendCooldown -= Time.deltaTime;
                if (descendCooldown <= 0)
                {
                    fly = false;
                    descendCooldown = 2f;
                }

            }

        }
        else if (flyCooldownStart > 0)
        {
            Cooldowns.text = "Fly burst cooldown: " + Math.Round(flyCooldownStart, 2).ToString();
            flyCooldownStart -= Time.deltaTime;

        }
        else if (flyCooldownStart <= 0 && fly == false) 
        {
            Cooldowns.text = "";
        }

    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, distToGround, groundMask);
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            Debug.Log("Jump");
            velocity.y = jumpHeight;
            characterController.Move(velocity * Time.deltaTime);
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
}