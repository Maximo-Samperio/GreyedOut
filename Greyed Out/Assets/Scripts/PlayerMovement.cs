using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    float horizontalMove = 0f;

    bool jump = false;

    public float runSpeed = 40f;
    void Start()
    {
        
    }

   
    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;    
        }

    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

}
