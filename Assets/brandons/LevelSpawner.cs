﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner main;

    public GameObject forwardTilePrefab;
    public GameObject leftTurnPrefab;
    public GameObject rightTurnPrefab;
    public GameObject jumpObstaclePrefab;
    public GameObject crouchObstaclePrefab;

    public Vector3 rotation = new Vector3();

    public bool spawnLeftTurn;

    private Transform nextSpawnLocation = null;

    private void Start()
    {
        main = this;

        // make the starting section
        GameObject newSection = Instantiate(forwardTilePrefab, new Vector3(0, -1, 0), Quaternion.identity, this.transform);
        nextSpawnLocation = newSection.transform.Find("EndPoint").transform;

        // make other sections
        for (int i = 0; i < 10; i++)
            SpawnNextTile();
    }



    public void SpawnNextTile()
    {
        GameObject newSection;
        int randomChoice = Random.Range(0, 100);

        
        if (randomChoice < 80)
        {
            if(randomChoice < 5)
            {
                newSection = Instantiate(jumpObstaclePrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
            }
            if(randomChoice < 10 && randomChoice >= 5)
            {
                newSection = Instantiate(crouchObstaclePrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
            }
            newSection = Instantiate(forwardTilePrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
        }
        else if (randomChoice < 90)
        {
            newSection = Instantiate(leftTurnPrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
            UpdateRotation(Vector3.left);
        }
        else
        {
            newSection = Instantiate(rightTurnPrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
            UpdateRotation(Vector3.right);
        }

        nextSpawnLocation = newSection.transform.Find("EndPoint").transform;

    }

    private void UpdateRotation(Vector3 direction)
    {
        // update the rotation direction
        if (direction == Vector3.right)
        {
            rotation += new Vector3(0, 90, 0);
        }
        else
        {
            rotation += new Vector3(0, -90, 0);
        }
    }
}
