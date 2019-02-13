using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore main;
    public float score;
  public Text scoreUI;
    public float modifier = 1;

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

  private void Update()
  {
    scoreUI.text = ((int)score).ToString();
  }

  private void FixedUpdate()
    {
        score += (PlayerSpeed.main.PlayerSpeedProperty * modifier) * Time.fixedDeltaTime;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("PlayerScore", score);
    }
}
