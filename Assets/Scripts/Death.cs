using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //For loaded next scene

public class Death : MonoBehaviour {

    [Tooltip("How long to wait before restarting the game")]
    public float waitTime = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        //Make sure we hit player
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Invoke("ResetGame", waitTime);
        }
    }

    /// <summary>
    /// Will restart the currently loaded level
    /// </summary>
    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
