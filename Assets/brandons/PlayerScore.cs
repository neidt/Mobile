using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public PlayerScore main;
    public float score;
  public Text scoreUI;
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
        score += PlayerSpeed.main.PlayerSpeedProperty * Time.fixedDeltaTime;
    }

    private void OnDestroy()
    {
        Debug.Log("Setting score");
        PlayerPrefs.SetFloat("PlayerScore", score);
    }
}
