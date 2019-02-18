using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * By Brandon Laing (JCCC)
 */

/// <summary>
/// Updates the rotation of the players camera
/// </summary>
public class PlayerCamera : MonoBehaviour
{
  /// <summary>
  /// Current rotation of the player camera
  /// </summary>
  private Vector3 rotation;

  public void UpdateRotation(Vector3 direction)
  {
    if (direction == Vector3.left)
      rotation += new Vector3(0, -90, 0);
    else
      rotation += new Vector3(0, 90, 0);

    transform.rotation = Quaternion.Euler(rotation);
  }
}
