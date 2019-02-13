using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
  public Transform player;
  public Vector3 rotation;

  public void UpdateRotation(Vector3 direction)
  {
    if (direction == Vector3.left)
      rotation += new Vector3(0, -90, 0);
    else
      rotation += new Vector3(0, 90, 0);

    transform.rotation = Quaternion.Euler(rotation);
  }
}
