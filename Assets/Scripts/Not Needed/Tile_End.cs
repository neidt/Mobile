using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles spawning a new tile and destroying this one when the player reaches the end
/// </summary>
public class Tile_End : MonoBehaviour {

    [Tooltip("How much time to wait before destroying the tile after reaching the end")]
    public float destroyTime = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        //Check that we collided with player
        if(other.gameObject.CompareTag("Player"))
        {
            //If so, spawn new tile
            Tile_Spawner.instance.SpawnNextTile();

            Destroy(transform.parent.gameObject, destroyTime);
            
        }
    }
}
