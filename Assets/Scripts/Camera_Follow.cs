using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adjusts the camera to follow and face a target
/// </summary>
public class Camera_Follow : MonoBehaviour {

    [Tooltip("What object the camera should be looking at")]
    public Transform target;

    [Tooltip("How offset will the camera be to the target")]
    public Vector3 offset = new Vector3(0, 3, -6);

	/// <summary>
    /// Update is called once per frame
    /// </summary>
	void Update () {

        //Check if target is a valid object
        if (target != null)
        {
            transform.position = target.position + offset;

            transform.LookAt(target);
        }
		
	}
}
