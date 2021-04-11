/*
 * File:	PlayerController.cs
 *
 * Author: Mara Dusevic (s200494@students.aie.edu.au)
 * Date Created: Friday 9 April 2021
 * Date Last Modified: Monday 12 April 2021
 *
 * Applied to the player to allow for movement.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Stores the components of the player
    private CharacterController controller;
    private Animator animator;
    private Rigidbody rb;

    // Stores player speed
    private float speed = 2.0f;

    // Speed when walking
    [SerializeField]
    public float walkSpeed = 18.0f;

    // Speed when running
    [SerializeField]
    public float runSpeed = 18.0f;

    // Gravity that affects the player
    [SerializeField]
    public float gravity = 8.0f;

    // Max height the player can jump
    [SerializeField]
    public float jumpHeight = 10.0f;

    // Distance to the ground
    [SerializeField]
    public float groundDistance = 0.4f;

    // Uses an object to determine if grounded
    [SerializeField]
    public Transform groundCheck;

    // The layer to check if grounded against
    [SerializeField]
    public LayerMask groundMask;

    // Used to set the objects velocity 
    private Vector3 velocity;

    // Used to determine if player is grounded or jumping
    private bool isGrounded = true;
    private bool isJumping = false;

    // Start Function
    void Start()
    {
        // Sets components of player to set variables
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update Function
    void Update()
    {
        // Creates a sphere to check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~0);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false;
        }

        // Gets input from the set movement keys
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Creates a move vector to apply to the object
        Vector3 move = transform.right * x + transform.forward * z;

        // Sets player to walk if its moving and shift isn't pressed
        if (move != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        // Sets player to run if its moving and shift is pressed
        else if (move != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        
        // Sets velocity of player when its not jumping and space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            velocity.y = jumpHeight;
            isJumping = true;
        }

        // Moves character via character controller and sets animation variable for movement
        controller.Move(move * speed * Time.deltaTime);
        animator.SetFloat("PosX", Input.GetAxis("Horizontal"));

        // Moves character vertically via character controller
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // If player is jumping, play jump animation
        animator.SetBool("IsJumping", isJumping);
    }

    // Sets movement speed to walk and plays walk animation
    private void Walk()
    {
        speed = walkSpeed;
        animator.SetFloat("PosY", Input.GetAxis("Vertical"));
        if (animator.GetFloat("PosY") > 0.8f)
        {
            animator.SetFloat("PosY", 0.8f);
        }
    }

    // Sets movement speed to run and plays run animation
    private void Run()
    {
        speed = runSpeed;
        animator.SetFloat("PosY", Input.GetAxis("Vertical"));
    }
}