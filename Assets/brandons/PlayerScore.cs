using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public PlayerScore main;
    public float score;

    private void Start()
    {
        main = this;
        if (PlayerPrefs.HasKey("PlayerScore"))
        {
            score = PlayerPrefs.GetFloat("PlayerScore");
        }
        else
        {
            PlayerPrefs.SetFloat("PlayerScore", 0);
        }
    }

    private void FixedUpdate()
    {
        score += PlayerSpeed.main.PlayerSpeedProperty * Time.fixedDeltaTime;
    }

    private void OnDestroy()
    {
        Debug.Log("Setting score");
        PlayerPrefs.SetFloat("PlayerScore", score);
    }
}
