using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Destroy : MonoBehaviour
{
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if we tapped the screen 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            //see if we tapped an obstacle
            if (Obstacle_Tap.TouchedObjects(touch) != null &&
                Obstacle_Tap.TouchedObjects(touch).tag == "Obstacle")
            {
                //if so, destoy the obstacle
                GameObject objectTouched = Obstacle_Tap.TouchedObjects(touch);
                GameObject particleThing = GameObject.Instantiate(explosion, Obstacle_Tap.TouchedObjects(touch).transform.position,Quaternion.identity);
                Destroy(particleThing, 2f);
                Destroy(objectTouched);
            }
        
        }
    }
}
