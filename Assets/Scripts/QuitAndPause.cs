using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAndPause : MonoBehaviour
{
    public Canvas pauseMenu;
    

    public void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
