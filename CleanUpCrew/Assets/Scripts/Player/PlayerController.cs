using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private CharacterController controller;

    [Header("Gravity")]
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float groundDistance;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private bool isCharacterGrounded = false;
    private Vector3 velocity = Vector3.zero;


    private Animator anim;

    private Vector3 moveDirection = Vector3.zero;
    private void Start()
    {
        GetReference();
        moveSpeed = walkSpeed;
    }
    private void GetReference()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {

        isCharacterGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        HandleJumping();
        HandleGravity();
        HandleRunning();
        HandleMovement();
        HandleAnimation();
    }
    private void HandleGravity()
    {

        if (isCharacterGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void HandleRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
        }
    }
    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);
        moveDirection = moveDirection.normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCharacterGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -2f * gravity);
            anim.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
        }
    }
    //Animations for running will be added later
    private void HandleAnimation()
    {
        if(moveDirection == Vector3.zero) 
        {
            anim.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
        }else if(moveDirection != Vector3.zero) 
        {
            anim.SetFloat("Speed", 1f, 0.2f, Time.deltaTime);
        }
    }


}


