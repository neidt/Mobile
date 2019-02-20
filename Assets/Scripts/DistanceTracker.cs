using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// @autor Brandon Laing
/// </summary>
public class DistanceTracker : MonoBehaviour
{
    /// <summary>
    /// Current distance the player has run this session
    /// </summary>
    private float currentDistance;

    /// <summary>
    /// Max distance the player has run any session
    /// </summary>
    private float maxDistance;

    /// <summary>
    /// Name for the max distance in player prefs
    /// </summary>
    private const string maxDistanceName = "MaxDistance";

    /// <summary>
    /// Tracks if we have already shown that the player has gotten a new high score
    /// </summary>
    private bool showedNewHighDistance = false;

    /// <summary>
    /// Canvas that holds the display of new high score reached
    /// </summary>
    [Tooltip("Canvas that holds message that the player has reached a new high score")]
    public GameObject NewHighScoreCanvas;

    /// <summary>
    /// Text display of the players current distance
    /// </summary>
    [Tooltip("Text to display current distance")]
    public Text DistanceDisplay;

    #region Natalie's Variables
    [Tooltip("Canvas for the upgrade menu")]
    public GameObject UpgradeCanvas;
    #endregion

    private void Start()
    {
        if (PlayerPrefs.HasKey(maxDistanceName))
            maxDistance = PlayerPrefs.GetFloat(maxDistanceName);
        else
        {
            PlayerPrefs.SetFloat(maxDistanceName, 0);
            maxDistance = 0;
        }
    }

    private void Update()
    {
        currentDistance += PlayerSpeed.main.Speed * Time.deltaTime;

        if (currentDistance > maxDistance && showedNewHighDistance == false)
        {
            DisplayedNewHighScore();
            showedNewHighDistance = true;
        }

        DistanceDisplay.text = ((int)currentDistance).ToString();
    }

    /// <summary>
    /// Displays that the player got a new high score
    /// </summary>
    private void DisplayedNewHighScore()
    {
        NewHighScoreCanvas.SetActive(!NewHighScoreCanvas.activeSelf);

        //#region Natalie Added
        //if (showedNewHighDistance)
        //{
        //    UpgradeCanvas.GetComponent<Canvas>().enabled = true;
        //}
        //else
        //{
        //    UpgradeCanvas.GetComponent<Canvas>().enabled = false;
        //}

        //#endregion

        if (showedNewHighDistance == false)
            Invoke("DisplayedNewHighScore", 5F);
    }

    private void OnDestroy()
    {
        if (currentDistance > maxDistance)
        {
            PlayerPrefs.SetFloat(maxDistanceName, currentDistance);
        }
    }
}
