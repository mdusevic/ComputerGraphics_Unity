/*
 * File:	CameraController.cs
 *
 * Author: Mara Dusevic (s200494@students.aie.edu.au)
 * Date Created: Friday 9 April 2021
 * Date Last Modified: Monday 12 April 2021
 *
 * Attached to the camera to follow a set target and move
 * according to the position of the mouse.
 *
 */

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    // Sets how sensitive the mouse is
    [SerializeField]
    public float mouseSensitivity = 1000.0f;

    // Takes in a target for the camera to follow
    [SerializeField]
    public GameObject target;

    // Minimum camera turning angle
    [SerializeField]
    public float minTurnAngle = -90.0f;

    // Maximum camera turning angle
    [SerializeField]
    public float maxTurnAngle = 0.0f;
    
    // Rotation of camera across the x-axis
    private float rotX;

    // Start Function
    void Start()
    {
        // Locks the cursor to screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update Function
    void Update()
    {
        // Gets the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Determines mouse's rotation
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90f, 90f);

        // Rotates camera based fromt the mouse's position
        transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
        target.transform.Rotate(Vector3.up * mouseX);
    }
}