using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Natalie Eidt
/// </summary>
public class RunGameButtons : MonoBehaviour
{
    /// <summary>
    /// reference to the pause menu canvas
    /// </summary>
    public Canvas pauseMenu;

    /// <summary>
    /// reference to the ghost powerup script
    /// </summary>
    public GhostPowerup ghostScript;

    /// <summary>
    /// reference to the double points scripts
    /// </summary>
    public DoublePointsEffect doublePointsScript;

    public void UpgradeGhostDuration()
    {
        ghostScript.ghostDuration += 2f;
    }

    public void UpgradeDoublePointsDuration()
    {
        doublePointsScript.duration += 2f;
    }

    public void Run()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        pauseMenu.gameObject.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;
    }
}
