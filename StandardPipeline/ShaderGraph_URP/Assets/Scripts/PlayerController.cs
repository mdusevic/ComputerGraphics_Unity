using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    Rigidbody rb;

    public float speed = 18.0f;
    public float gravity = 8.0f;
    public float jumpHeight = 10.0f;

    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded = true;

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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            velocity.y = jumpHeight;
        }

        velocity.y -= gravity * Time.deltaTime;
        animator.SetBool("IsGrounded", isGrounded);

        controller.Move(velocity * Time.deltaTime);

        animator.SetFloat("PosX", Input.GetAxis("Horizontal"));
        animator.SetFloat("PosY", Input.GetAxis("Vertical"));
    }
}