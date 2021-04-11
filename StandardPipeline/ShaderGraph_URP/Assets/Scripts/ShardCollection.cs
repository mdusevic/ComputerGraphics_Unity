/*
 * File:	ShardCollection.cs
 *
 * Author: Mara Dusevic (s200494@students.aie.edu.au)
 * Date Created: Sunday 11 April 2021
 * Date Last Modified: Monday 12 April 2021
 *
 * The script is used to manage the shards in game. All 
 * collected shards are also recorded via this script.
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardCollection : MonoBehaviour
{
    // Shards collected by the player
    private int shardsCollected = 0;

    // The max amount of shards that can be collected
    private int maxShards = 15;

    // Used to determine if a shard has been touched
    private bool isShardEntered = false;

    // Update Function
    private void Update()
    {
        if (isShardEntered)
        {
            AddShard();
            isShardEntered = false;
        }
    }

    // Returns the amount of shards collected
    public int GetShards()
    {
        return shardsCollected;
    }

    // Returns the max amount of shards that can be collected
    public int GetMaxShards()
    {
        return maxShards;
    }

    // Increases amount of shards collected when called
    private void AddShard()
    {
        shardsCollected++;
    }

    // Determines whether all the shards have been collected
    public bool IsEndGame()
    {
        if (shardsCollected >= maxShards && !isShardEntered)
        {
            return true;
        }

        return false;
    }

    // Used to determine when a shard has been collected
    private void OnTriggerEnter(Collider collider)
    {
        // If the object is a shard
        if (collider.gameObject.tag == "Shard")
        {
            isShardEntered = true;
            Destroy(collider.gameObject, 0.05f);
        }
    }
}
