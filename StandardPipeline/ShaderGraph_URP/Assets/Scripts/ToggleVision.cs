using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVision : MonoBehaviour
{
    public Camera camDefault;
    public Camera camVisioned;

    // Start is called before the first frame update
    void Start()
    {
        camDefault.enabled = true;
        camVisioned.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Vision();
        }
    }

    private void Vision()
    {
        camDefault.enabled = !camDefault.enabled;
        camVisioned.enabled = !camVisioned.enabled;
    }
}
