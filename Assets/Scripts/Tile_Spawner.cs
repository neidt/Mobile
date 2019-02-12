using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the spawning of tiles
/// </summary>
public class Tile_Spawner : MonoBehaviour {
    public static Tile_Spawner instance;

    [Tooltip("The tile to spawn")]
    public Transform tile;

    [Tooltip("Where the first tile should be placed")]
    public Vector3 startPoint = new Vector3(0, 0, -5);

    [Tooltip("How many tiles should be in the initial pool")]
    public int initialPool = 10;

    [Tooltip("What obstacle will be spawned")]
    public Transform obstacle;

    [Tooltip("How many initial tiles to spawn with no obstacles")]
    public int initialNoObstacles = 4;

    /// <summary>
    /// Where the next tile should be spawned;
    /// </summary>
    Vector3 nextTileLocation;

    /// <summary>
    /// How the next tile should be rotated
    /// </summary>
    private Quaternion nextTileRotation;

    /// <summary>
    /// Used for initilization
    /// </summary>
	void Start ()
    {
        instance = this;

        //Set the next starting point
        nextTileLocation = startPoint;
        nextTileRotation = Quaternion.identity;

        for(int i = 0; i < initialPool; i++)
        {
            SpawnNextTile(i >= initialNoObstacles);
        }
		
	}

    /// <summary>
    /// Will spawn a tile at a certain location and setup the next position
    /// </summary>
    public void SpawnNextTile(bool spawnObstacles = true)
    {
        var newTile = Instantiate(tile, nextTileLocation, nextTileRotation);

        var nextTile = newTile.Find("Next Spawn");
        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;

        if (!spawnObstacles)
            return;

        List<GameObject> obstacleSpawnPoints = new List<GameObject>();

        //Collect all the possible spawn points
        foreach(Transform child in newTile)
        {
            if(child.CompareTag("ObstacleSpawn"))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }
        }

        //Make sure we have at least 1
        if(obstacleSpawnPoints.Count > 0)
        {
            //Debug.Log("Spawn an obstacle");

            int selectedPoint = Random.Range(0, obstacleSpawnPoints.Count);

            //Store the position
            Vector3 spawnPos = obstacleSpawnPoints[selectedPoint].transform.position;

            var newObstacle = Instantiate(obstacle, spawnPos, Quaternion.identity);

            newObstacle.SetParent(obstacleSpawnPoints[selectedPoint].transform);
        }
    }
}
