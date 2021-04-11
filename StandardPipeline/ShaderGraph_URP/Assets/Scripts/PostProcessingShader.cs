/*
 * File:	PostProcessing.cs
 *
 * Author: Mara Dusevic (s200494@students.aie.edu.au)
 * Date Created: Thursday 8 April 2021
 * Date Last Modified: Monday 12 April 2021
 *
 * Applied to post-processing to the camera its attached to.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingShader : MonoBehaviour
{
    // Material applied to camera
    public Material mat;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, mat);
    }
}
