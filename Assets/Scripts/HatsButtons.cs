using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * By Brandon Laing (JCCC)
 * this will control that hats system
 */
public class HatsButtons : MonoBehaviour
{
  public GameObject greenHat, purpleHat;
  public bool greenHatBought = false, purpleHatBought = false;

  private void Start()
  {
    if (!PlayerPrefs.HasKey("GreenBought"))
      PlayerPrefs.SetInt("GreenBought", 0);

    else if (PlayerPrefs.GetInt("GreenBought") == 1)
      greenHatBought = true;

    if (!PlayerPrefs.HasKey("PurpleBought"))
      PlayerPrefs.SetInt("PurpleBought", 0);

    else if (PlayerPrefs.GetInt("PurpleBought") == 1)
      purpleHatBought = true;
  }

  /// <summary>
  /// Spends score to buy a green hat and puts it on the players head
  /// </summary>
  public void BuyGreenHat()
  {
    if (greenHatBought)
    {
      if (purpleHat.activeSelf)
        purpleHat.SetActive(false);
      greenHat.SetActive(true);
    }

    else if (PlayerScore.main.Score >= 100)
    {
      PlayerScore.main.Score -= 100;
      PlayerPrefs.SetInt("GreenBought", 1);
      greenHatBought = true;

      if (purpleHat.activeSelf)
        purpleHat.SetActive(false);
      greenHat.SetActive(true);
    }
  }

  /// <summary>
  /// Spends score to buy a purple hat and puts it on the players head
  /// </summary>
  public void BuyPurpleHat()
  {
    if (purpleHatBought)
    {
      if (greenHat.activeSelf)
        greenHat.SetActive(false);
      purpleHat.SetActive(true);
    }

    else if (PlayerScore.main.Score >= 100)
    {
      PlayerScore.main.Score -= 100;
      PlayerPrefs.SetInt("PurpleBought", 1);
      purpleHatBought = true;

      if (greenHat.activeSelf)
        greenHat.SetActive(false);
      purpleHat.SetActive(true);
    }
  }

  /// <summary>
  /// Resets the playerprefs and unbuys all hats
  /// </summary>
  public void ResetPlayerPrefs()
  {
    PlayerPrefs.SetInt("GreenBought", 0);
    PlayerPrefs.SetInt("PurpleBought", 0);

    greenHatBought = false; purpleHatBought = false;
    greenHat.SetActive(false);
    purpleHat.SetActive(false);
  }

}