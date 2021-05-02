using System.Collections;
using System.Collections.Generic;
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
    public Text debug;
    public Transform groundCheck;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    public float FlyHeight = 20f;
    private bool fly = false;
    private float flyStart;

    void FlyAbility() 
    {
        if (Input.GetButtonDown("Fly"))
        {
            flyStart = groundCheck.position.y;
            velocity.y = MovementSpeed * 10;
            characterController.Move(velocity * Time.deltaTime); 
        
        }
        fly = true;    }

    
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
        if (fly) {
            if ((groundCheck.position.y - flyStart) > FlyHeight)
            {
                velocity.y = 0f;
                characterController.Move(velocity * Time.deltaTime);
                fly = false;

            }

        }
       
    }
}