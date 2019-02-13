using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionDestroyer : MonoBehaviour
{
  /// <summary>
  /// Specified amount of wait time till destroyed
  /// </summary>
  [Tooltip("Specified amount of wait time till destroyed")]
  public float destoryDelay = 3.5F;

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      LevelSpawner.main.SpawnNextTile();

      Destroy(this.transform.parent.gameObject, destoryDelay);
    }
  }
}
