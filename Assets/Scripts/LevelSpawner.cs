using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * By Brandon Laing (JCCC)
 * This will manage the placement of tiles
 */

/// <summary>
/// Spawns tiles for the player to run on
/// </summary>
public class LevelSpawner : MonoBehaviour
{
  /// <summary>
  /// Current version of level spawner in a level.
  /// </summary>
  public static LevelSpawner main;

  [Header("Running object prefabs")]
  public GameObject forwardTilePrefab;
  public GameObject leftTurnPrefab;
  public GameObject rightTurnPrefab;

  [Header("Obstacle prefabs")]
  public GameObject jumpObstaclePrefab;
  public GameObject crouchObstaclePrefab;

  /// <summary>
  /// Number of tiles spawned at a time
  /// </summary>
  [Tooltip("Number of tiles spawned at a time")]
  public int numberOfTilesAtATime = 10;

  /// <summary>
  /// Number oF tiles needed to turn
  /// </summary>
  [Tooltip("Number oF tiles needed to turn")]
  public int numberOfForwardToTurn = 5;

  /// <summary>
  /// Count of how many tiles that have been placed in a row
  /// </summary>
  private int forwardTilesInARow = 0;
  
  /// <summary>
  /// Current rotation of the level
  /// </summary>
  private Vector3 rotation = new Vector3();

  /// <summary>
  /// Location of the next tile
  /// </summary>
  private Transform nextSpawnLocation = null;

  private void Start()
  {
    if (main != null)
      Destroy(this);
    else
      main = this;

    MakeStartingSection();

    // make the first 10 
    for (int i = 0; i < (numberOfTilesAtATime - 1); i++)
      SpawnNextTile();
  }

  /// <summary>
  /// Makes the starting section of running space
  /// </summary>
  private void MakeStartingSection()
  {
    GameObject newSection = Instantiate(forwardTilePrefab, new Vector3(0, -1, 0), Quaternion.identity, this.transform);
    nextSpawnLocation = newSection.transform.Find("EndPoint").transform;
  }

  /// <summary>
  /// Spawns the next tile in the chain
  /// </summary>
  public void SpawnNextTile()
  {
    GameObject newSection;
    if (forwardTilesInARow <  numberOfForwardToTurn)
    {
      newSection = MakeRandomForwardTile();
    }
    else
    {
      int randomNumber = Random.Range(0, 100);

      if (randomNumber < 80)
        newSection = MakeRandomForwardTile();
      else
        newSection = MakeRandomTurn();
    }

    nextSpawnLocation = newSection.transform.Find("EndPoint").transform;
  }

  /// <summary>
  /// Makes a new random forward tile
  /// </summary>
  /// <returns>Returns the new section of the path</returns>
  private GameObject MakeRandomForwardTile()
  {
    forwardTilesInARow++;
    int randomNumber = Random.Range(0, 100);

    if (randomNumber < 80)
    {
      GameObject newSection = Instantiate(forwardTilePrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
      return newSection;
    }
    else if (randomNumber < 90)
    {
      GameObject newSection = Instantiate(jumpObstaclePrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
      return newSection;
    }
    else
    {
      GameObject newSection = Instantiate(crouchObstaclePrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
      return newSection;
    }
  }

  /// <summary>
  /// Makes a new random turn
  /// </summary>
  /// <returns>Returns the left of right turn that is generated</returns>
  private GameObject MakeRandomTurn()
  {
    forwardTilesInARow = 0;
    int randomNumber = Random.Range(1, 3);

    if (randomNumber == 1)
    {
      GameObject newSection = Instantiate(leftTurnPrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
      UpdateRotation(Vector3.left);
      return newSection;
    }
    else
    {
      GameObject newSection = Instantiate(rightTurnPrefab, nextSpawnLocation.position, Quaternion.Euler(rotation), this.transform);
      UpdateRotation(Vector3.right);
      return newSection;
    }

  }

  /// <summary>
  /// Changes the rotation of the tiles
  /// </summary>
  /// <param name="direction">Takes vector3.right or left</param>
  private void UpdateRotation(Vector3 direction)
  {
    // update the rotation direction
    if (direction == Vector3.right)
      rotation += new Vector3(0, 90, 0);

    else if (direction == Vector3.left)
      rotation += new Vector3(0, -90, 0);

    else
      Debug.LogWarning("Tried to update rotation and didn't receive proper input");
  }
}
