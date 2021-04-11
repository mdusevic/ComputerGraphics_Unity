/*
 * File:	ToggleVision.cs
 *
 * Author: Mara Dusevic (s200494@students.aie.edu.au)
 * Date Created: Saturday 10 April 2021
 * Date Last Modified: Monday 12 April 2021
 *
 * Used to determine which camera to use. One camera is
 * normal, the other applies post-processing.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVision : MonoBehaviour
{
    // Default camera for scene
    [SerializeField]
    public Camera camDefault;

    // Post-processed camera for scene
    [SerializeField]
    public Camera camVisioned;

    // Start Function
    void Start()
    {
        camDefault.enabled = true;
        camVisioned.enabled = false;
    }

    // Update Function
    void Update()
    {
        // Enables post-processed camera
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vision();
        }
    }

    // Enables and disables the cameras
    public void Vision()
    {
        camDefault.enabled = !camDefault.enabled;
        camVisioned.enabled = !camVisioned.enabled;
    }
}