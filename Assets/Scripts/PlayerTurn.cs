using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * By Brandon Laing (JCCC)
 */

/// <summary>
/// This will manage whether the play can turn or not
/// </summary>
public class PlayerTurn : MonoBehaviour
{
  /// <summary>
  /// Current instance of PlayerTurn
  /// </summary>
  public static PlayerTurn main;

  /// <summary>
  /// List of colliders that let the player turn left
  /// </summary>
  private List<Transform> leftColliders = new List<Transform>();

  /// <summary>
  /// checks if there are any left colliders
  /// </summary>
  private bool CanTurnLeft
  {
    get
    {
      return leftColliders.Count > 0;
    }
  }

  /// <summary>
  /// list of colliders that let the player turn right
  /// </summary>
  private List<Transform> rightColliders = new List<Transform>();

  /// <summary>
  /// Checks if there are any right colliders
  /// </summary>
  private bool CanTurnRight
  {
    get
    {
      return rightColliders.Count > 0;
    }
  }

  /// <summary>
  /// Users start touch position
  /// </summary>
  private Vector3 touchStart;

  public float minPixleDifference = 10;

  /// <summary>
  /// resolution divided this number
  /// </summary>
  [Tooltip("The screen size / this number for distance to swipe")]
  public float minSwipeDistance = 1.3F;

  /// <summary>
  /// Direction the player is running
  /// </summary>
  [Tooltip("Direction the player is running")]
  public Vector3 moveDirection = Vector3.forward;

  private void Start()
  {
    main = this;
  }

  private void Update()
  {
    if (Input.touchCount > 0)
    {
      // get the touches start then exit from the loop
      if (Input.touches[0].phase == TouchPhase.Began)
      {
        touchStart = Input.touches[0].position;
        return;
      }

      if (Input.touches[0].phase == TouchPhase.Ended)
      {
        float distance = Input.touches[0].position.x - touchStart.x;
        distance = Mathf.Abs(distance);

#if UNITY_STANDALONE || UNITY_WEBPLAYER
        if (distance < Camera.main.pixelWidth / minSwipeDistance)
          return;

#elif UNITY_IOS || UNITY_ANDROID
        // I'm not sure if i should be using Camera.main.pixelWidth or Screen.currentResolution.width
        if (distance < Camera.main.pixelWidth / minSwipeDistance)
          return;
#endif
        // if the touch went left check && the player can turn left turn left and update stuff
        if (touchStart.x > Input.touches[0].position.x)
        {
          if (CanTurnLeft)
          {
            UpdateDirection(Vector3.left);
            Camera.main.GetComponentInParent<PlayerCamera>().UpdateRotation(Vector3.left);
            transform.eulerAngles += new Vector3(0, -90, 0);
          }
          else
            Die.PlayerDie();
        }

        // if the touch went right check && the player can turn right turn right and update stuff
        else if (touchStart.x < Input.touches[0].position.x)
        {
          if (CanTurnRight)
          {
            UpdateDirection(Vector3.right);
            Camera.main.GetComponentInParent<PlayerCamera>().UpdateRotation(Vector3.right);
            transform.eulerAngles += new Vector3(0, 90, 0);
          }
          else
            Die.PlayerDie();
        }
      }
    }
  }

  /// <summary>
  /// Sets the new direction the player is running
  /// </summary>
  /// <param name="direction">Vector3.right or left depending on which way you are turning</param>
  private void UpdateDirection(Vector3 direction)
  {
    if (moveDirection == Vector3.forward)
    {
      moveDirection = direction;
      return;
    }

    if (moveDirection == Vector3.left)
    {
      if (direction == Vector3.left)
        moveDirection = Vector3.back;
      else
        moveDirection = Vector3.forward;
      return;
    }

    if (moveDirection == Vector3.right)
    {
      if (direction == Vector3.left)
        moveDirection = Vector3.forward;
      else
        moveDirection = Vector3.back;
      return;
    }

    if (moveDirection == Vector3.back)
    {
      if (direction == Vector3.left)
        moveDirection = Vector3.right;
      else
        moveDirection = Vector3.left;
      return;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("RightTurn") && !rightColliders.Contains(other.transform))
    {
      rightColliders.Add(other.transform);
    }
    else if (other.CompareTag("LeftTurn") && !leftColliders.Contains(other.transform))
    {
      leftColliders.Add(other.transform);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("RightTurn") && rightColliders.Contains(other.transform))
    {
      rightColliders.Remove(other.transform);
    }
    else if (other.CompareTag("LeftTurn") && leftColliders.Contains(other.transform))
    {
      leftColliders.Remove(other.transform);
    }
  }
}
