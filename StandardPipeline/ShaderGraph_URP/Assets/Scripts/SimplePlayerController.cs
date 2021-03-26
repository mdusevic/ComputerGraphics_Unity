using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("IsAiming", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsAiming", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("IsJumping", false);
        }
    }
}
