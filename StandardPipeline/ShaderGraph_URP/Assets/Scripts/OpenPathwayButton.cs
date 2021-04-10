using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPathwayButton : MonoBehaviour
{
    [SerializeField]
    public GameObject pathway;

    private Material dissolveMat;
    private Transform[] path;

    private bool dissolvePathway;
    private float prevLerp = 1;

    private void Start()
    {
        dissolvePathway = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 13;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float maxDistance = 10.0f;

            if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    path = pathway.GetComponentsInChildren<Transform>();

                    dissolvePathway = true;
                }
            }
        }

        if (dissolvePathway && path.Length > 0)
        {
            foreach (Transform piece in path)
            {
                if (piece != path[0])
                {
                    piece.GetComponent<BoxCollider>().enabled = true;
                    dissolveMat = piece.GetComponent<MeshRenderer>().material;
                    prevLerp = Mathf.Lerp(prevLerp, 0, 0.1f * Time.deltaTime);
                    dissolveMat.SetFloat("_Dissolve", prevLerp);
                }
            }

            if (dissolveMat.GetFloat("_Dissolve") <= 0)
            {
                dissolvePathway = false;
                path = null;
                dissolveMat = null;
                Destroy(this.gameObject);
            }
        }
    }
}
