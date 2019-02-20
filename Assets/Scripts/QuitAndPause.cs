using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Natalie Eidt
/// </summary>
public class QuitAndPause : MonoBehaviour
{
    /// <summary>
    /// reference to the pause menu canvas
    /// </summary>
    public Canvas pauseMenu;
    
    /// <summary>
    /// shows the canvas and pauses the time
    /// </summary>
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
