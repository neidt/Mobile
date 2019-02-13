using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatsButtons : MonoBehaviour
{
    public GameObject greenHat, purpleHat;

    public void BuyGreenHat()
    {
        if (PlayerScore.main.score >= 100)
        {
            PlayerScore.main.score -= 100;
            if (purpleHat.activeSelf)
                purpleHat.SetActive(false);
            greenHat.SetActive(true);
        }
    }

    public void BuyPurpleHat()
    {
        if (PlayerScore.main.score >= 100)
        {
            PlayerScore.main.score -= 100;
            if (greenHat.activeSelf)
                greenHat.SetActive(false);
            purpleHat.SetActive(true);
        }
    }
}
