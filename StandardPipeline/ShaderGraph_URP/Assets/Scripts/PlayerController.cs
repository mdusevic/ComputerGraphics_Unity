using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    Rigidbody rb;

    public float speed = 2.0f;
    public float walkSpeed = 18.0f;
    public float runSpeed = 18.0f;
    public float gravity = 8.0f;
    public float jumpHeight = 10.0f;

    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded = true;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, ~0);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            isJumping = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (move != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if (move != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            velocity.y = jumpHeight;
            isJumping = true;
        }

        controller.Move(move * speed * Time.deltaTime);
        animator.SetFloat("PosX", Input.GetAxis("Horizontal"));

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        animator.SetBool("IsJumping", isJumping);
    }

    private void Walk()
    {
        speed = walkSpeed;
        animator.SetFloat("PosY", Input.GetAxis("Vertical"));
        if (animator.GetFloat("PosY") > 0.8f)
        {
            animator.SetFloat("PosY", 0.8f);
        }
    }

    private void Run()
    {
        speed = runSpeed;
        animator.SetFloat("PosY", Input.GetAxis("Vertical"));
    }
}