using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCollection : MonoBehaviour
{
    private int shardsCollected = 0;

    private void Update()
    {
        if (shardsCollected == 15)
        {
            Time.timeScale = 0.0f;
            Debug.Log("GameOver");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            shardsCollected++;
            Debug.Log(shardsCollected);
            Destroy(collision.gameObject, 0.2f);
        }
    }
}
