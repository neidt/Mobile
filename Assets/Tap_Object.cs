using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///detect if we tapped on a game object
/// </summary>
public class Obstacle_Tap : MonoBehaviour
{
    /// <summary>
    /// returns the game object that was touched
    /// </summary>
    /// <param name="touch"> the point at which we are touching </param>
    /// <returns></returns>
    public static GameObject TouchedObjects(Touch touch)
    {
        //convert where we touched the screen to a ray
        Ray touchRay = Camera.main.ScreenPointToRay(touch.position);

        RaycastHit hit;
        if(Physics.Raycast(touchRay, out hit))
        {
            return hit.collider.gameObject;
        }

        //we didnt hit anything
        return null;
    }
}
