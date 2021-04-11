/*
 * File:	OpenPathwayButton.cs
 *
 * Author: Mara Dusevic (s200494@students.aie.edu.au)
 * Date Created: Sunday 11 April 2021
 * Date Last Modified: Monday 12 April 2021
 *
 * Applied to buttons in scene in which when interacted with
 * enables a specific pathway to appear.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPathwayButton : MonoBehaviour
{
    // Pathway the button opens
    [SerializeField]
    public GameObject pathway;

    // Used to change the buttons material
    [SerializeField]
    public Material clickedMat;

    // Used to change the button material to its original
    private Material normalMat;

    // Used to edit the pathways dissolve shader
    private Material dissolveMat;
    
    // Array that stores all children of a pathway
    private Transform[] path;

    // Used to enable the dissolve of a pathway
    private bool dissolvePathway;

    // Stores the previous value of the dissolve effect
    private float prevLerp = 1;

    // Time taken before material changes back
    private float timeToColor = 0.1f;

    // Start Function
    private void Start()
    {
        // Getting the buttons original material
        normalMat = GetComponent<MeshRenderer>().material;
        dissolvePathway = false;
    }

    // Update Function
    private void Update()
    {
        // When right mouse click is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Raycasts from the main camera between specific layers to find the button
            int layerMask = 1 << 13;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float maxDistance = 10.0f;

            // If an object has been hit
            if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
            {
                // Checks if object is an object containing this script
                if (hit.collider.gameObject == this.gameObject)
                {
                    // Stores all pieces of the pathway in an array
                    path = pathway.GetComponentsInChildren<Transform>();

                    // Changes button material
                    StartCoroutine("ChangeMaterial");

                    // Allows for pathway to appear
                    dissolvePathway = true;
                }
            }
        }

        // When the pathway is allowed to appear and isn't empty
        if (dissolvePathway && path.Length > 0)
        {
            // Loops through all objects in the array
            foreach (Transform piece in path)
            {
                // Grabs all objects in the array other than the parent
                if (piece != path[0])
                {
                    // Enables collider to allow for the player to walk on it
                    piece.GetComponent<BoxCollider>().enabled = true;

                    // Edits dissolve value of the pathway to change over time
                    // Every update it grabs the previous value before minusing from the value
                    dissolveMat = piece.GetComponent<MeshRenderer>().material;
                    prevLerp = Mathf.Lerp(prevLerp, 0, 0.12f * Time.deltaTime);
                    dissolveMat.SetFloat("_Dissolve", prevLerp);
                }
            }

            // If the dissolve value reaches 0
            if (dissolveMat.GetFloat("_Dissolve") <= 0)
            {
                // Pathway no longer needs to be dissolved
                dissolvePathway = false;

                // Clear variables
                path = null;
                dissolveMat = null;
                
                // Removes button from scene after a certain amount of time
                Destroy(gameObject, 0.2f);
            }
        }
    }

    // Changes material of button
    IEnumerator ChangeMaterial()
    {
        // Sets the buttons material to its new one
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = clickedMat;

        // Calls a timer in which will continue after the set amount of time
        yield return new WaitForSeconds(timeToColor);

        // Sets back to original material
        mr.material = normalMat;

        // Disables mesh renderer and collider to hide button
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<SphereCollider>().enabled = false;
    }
}
