using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAndPause : MonoBehaviour
{
    public Canvas pauseMenu;
    

    public void PauseGame()
    {
        pauseMenu.gameObject.GetComponent<Canvas>().enabled = true;
        Time.timeScale = 0.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
