using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * By Brandon Laing (JCCC)
 */

/// <summary>
/// This will track and update the speed of the player
/// </summary>
public class PlayerSpeed : MonoBehaviour
{
  /// <summary>
  /// Current version in use
  /// </summary>
  public static PlayerSpeed main;

  private float _playerSpeed = 5;
  public float Speed
  {
    get { return _playerSpeed; }
    set
    {
      if (value > maxPlayerSpeed)
        _playerSpeed = maxPlayerSpeed;
      else
        _playerSpeed = value;
    }
  }

  /// <summary>
  /// Amount the player speed increases on fixed update * fixed delta time
  /// </summary>
  [Tooltip("Amount the player speed increases per second")]
  public float playerSpeedIncreasePerSecond = 5F;

  /// <summary>
  /// Max speed the player can reach
  /// </summary>
  [Tooltip("Max speed the player can reach")]
  public float maxPlayerSpeed = 25;

  /// <summary>
  /// Speed that the player starts at
  /// </summary>
  [Tooltip("Speed that the player starts at")]
  public float startingSpeed;

  private void Start()
  {
    main = this;
    Speed = startingSpeed;
  }

  private void FixedUpdate()
  {
    Speed += playerSpeedIncreasePerSecond * Time.fixedDeltaTime;
  }
}