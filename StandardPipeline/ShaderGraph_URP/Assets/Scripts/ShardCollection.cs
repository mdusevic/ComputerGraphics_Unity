using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCollection : MonoBehaviour
{
    private int shardsCollected = 0;
    private int maxShards = 15;
    private bool isShardEntered = false;

    private void Update()
    {
        if (isShardEntered)
        {
            AddShard();
            isShardEntered = false;
        }
    }

    public int GetShards()
    {
        return shardsCollected;
    }

    public int GetMaxShards()
    {
        return maxShards;
    }

    private void AddShard()
    {
        shardsCollected++;
    }

    public bool IsEndGame()
    {
        if (shardsCollected >= maxShards && !isShardEntered)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Shard")
        {
            isShardEntered = true;
            Destroy(collider.gameObject, 0.05f);
        }
    }
}
