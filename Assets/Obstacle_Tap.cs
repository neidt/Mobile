using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///detect if we tapped on a game object
/// </summary>
public class Tap_Object : MonoBehaviour
{
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
