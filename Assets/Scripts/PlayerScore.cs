using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * By Brandon Laing (JCCC)
 * One thing I'm not sure if its ok is saving and loading score from player prefs. I tried find if playerPrefs uses lots of memory
 * to save and load. I have it both ways hoping that was OK.
 */

/// <summary>
/// Tracks the players score
/// </summary>
public class PlayerScore : MonoBehaviour
{
  /// <summary>
  /// Current instance of playerScore
  /// </summary>
  public static PlayerScore main;

  /// <summary>
  /// Players score saved into player prefs
  /// </summary>
  public float Score
  {
    get
    {
      if (!PlayerPrefs.HasKey("PlayerScore"))
      {
        PlayerPrefs.SetFloat("PlayerScore", 0);
      }

      return PlayerPrefs.GetFloat("PlayerScore");
    }
    set
    {
      PlayerPrefs.SetFloat("PlayerScore", value);
    }
  }

  [Tooltip("UI text element to display score")]
  public Text scoreUI;

  /// <summary>
  /// score per second * modifier
  /// </summary>
  public float modifier = 1;

  private void Start()
  {
    main = this;

    // See top comment for explanation on all the commented out code
    //if (PlayerPrefs.HasKey("PlayerScore"))
    //{
    //  Score = PlayerPrefs.GetInt("PlayerScore");
    //}
    //else
    //{
    //  PlayerPrefs.SetFloat("PlayerScore", 0);
    //}
  }

  private void Update()
  {
    if (scoreUI != null)
      scoreUI.text = ((int)Score).ToString();
    else
      Debug.LogWarning("There is no score UI set");
  }

  private void FixedUpdate()
  {
    Score += (PlayerSpeed.main.Speed * modifier) * Time.fixedDeltaTime;
  }

  //private void OnDestroy()
  //{
  //  PlayerPrefs.SetFloat("PlayerScore", score);
  //}
}
