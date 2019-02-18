using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
  public static void PlayerDie()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
  }
}
