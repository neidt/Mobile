using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner main;

    public GameObject forwardTilePrefab;
    public GameObject leftTilePrefab;
    public GameObject rightTilePrefab;
    public Vector3 rotation;

    private Transform nextSpawnLocation = null;

    private void Start()
    {
        main = this;
        for (int i = 0; i < 15; i++)
            SpawnNextTile();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnNextTile();
    }

    private void SpawnNextTile()
    {
        GameObject newSection;
        if (nextSpawnLocation == null)
        {
            newSection = Instantiate(forwardTilePrefab, new Vector3(0, -1, 0), Quaternion.identity, this.transform);
        }
        else
        {
            newSection = Instantiate(forwardTilePrefab, nextSpawnLocation.position, Quaternion.identity, this.transform);
        }

        nextSpawnLocation = newSection.transform.Find("EndPoint").transform;

    }
}
