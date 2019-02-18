using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPowerup : MonoBehaviour
{
    /// <summary>
    /// reference to the player's game object
    /// </summary>
    GameObject playerObj;

    /// <summary>
    /// reference to the materials to switch between when going ghost
    /// </summary>
    public Material ghostMat;
    public Material ethanMat;

    /// <summary>
    /// the duration that the mode lasts
    /// </summary>
    public float ghostDuration = 5f;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(EnableGhostMode());
        }
    }

    /// <summary>
    /// starts the ghost powerup
    /// </summary>
    /// <returns> the duration of the powerup </returns>
    IEnumerator EnableGhostMode()
    {
        playerObj.GetComponent<CapsuleCollider>().enabled = false;
        playerObj.GetComponent<Rigidbody>().useGravity = false;
        playerObj.GetComponentInChildren<SkinnedMeshRenderer>().material = ghostMat;

        yield return new WaitForSecondsRealtime(ghostDuration);

        playerObj.GetComponent<CapsuleCollider>().enabled = true;
        playerObj.GetComponent<Rigidbody>().useGravity = true;
        playerObj.GetComponentInChildren<SkinnedMeshRenderer>().material = ethanMat;
    }

    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
}
