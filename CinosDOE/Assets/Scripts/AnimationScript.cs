using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator w_Animator;
    // Start is called before the first frame update
    void Start()
    {
        w_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        w_Animator.SetBool("IsWalking", isWalking);
        if (Input.GetButtonDown("Fire1")) {
            w_Animator.SetTrigger("Fire");
        }
        
    }
}
