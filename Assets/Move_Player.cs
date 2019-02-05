using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for moving the player automatically and receiving input.
/// </summary>
/// 

public enum INPUT_TYPE
{
    Accelerometer,
    ScreenTouch
}

[RequireComponent(typeof(Rigidbody))]
public class Move_Player : MonoBehaviour
{
    /// <summary>
    /// A reference to the Rigidbody component
    /// </summary>
    Rigidbody rb;

    [Tooltip("How fast the ball moves left/right")]
    public float dodgeSpeed = 5f;

    [Tooltip("How fast the ball rolls forward (automatically)")]
    [Range(0, 10)]
    public float rollSpeed = 5;

    [Header("Movement Options")]
    [Tooltip("WHat type of movement detection will be used")]
    public INPUT_TYPE inputType = INPUT_TYPE.Accelerometer;


    /// <summary>
    /// How much force should be applied in the horizontal axis
    /// </summary>
    float horizontalSpeed;

    /// <summary>
    /// the stored forward velocity of the ball between frames
    /// </summary>
    private Vector3 forwardMovement;

    [Header("Swipe Properties")]
    [Tooltip("How far the player will move when they swipe")]
    public float swipeMove = 2f;

    [Tooltip("How far player must swipe to do action (in pixel space)")]
    public float minSwipeDistance = 1f;

    /// <summary>
    /// stores the starting pos of the mobile touch event
    /// </summary>
    private Vector2 touchStart;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update it called by frame
    /// </summary>
    void Update()
    {
#if UNITY_STANDALONE || UNITY_WEBPLAYER

        //if mouse button is held down (screen is tapped)
        if (Input.GetMouseButtonDown(0))
        {
            horizontalSpeed = CalculateMovement(Input.mousePosition);
        }
#elif UNITY_IOS || UNITY_ANDROID

        //check for input type
        if(inputType == INPUT_TYPE.Accelerometer)
        {
            //move based on accelerometer
            horizontalSpeed = Input.acceleration.x * dodgeSpeed;
        }

        //use the real android touch system
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.touches[0];

            if(inputType == INPUT_TYPE.ScreenTouch)
            {
                horizontalSpeed = CalculateMovement(firstTouch.position);
            }
            
            SwipeTeleport(firstTouch);
        }
#endif
    }

    void SwipeTeleport(Touch touch)
    {
        //check if touch has just started
        if(touch.phase  == TouchPhase.Began)
        {
            touchStart = touch.position;
        }

        //check if touch was just ended
        else if(touch.phase == TouchPhase.Ended)
        {
            //get the position we ended the touch
            Vector2 touchEnd = touch.position;

            //calculate difference btwn start and end of touch
            float xDiff = touchEnd.x - touchStart.x;

            if(Mathf.Abs(xDiff) < minSwipeDistance)
            {
                return;
            }

            Vector3 moveDir;
            if (xDiff < 0)
            {
                moveDir = Vector3.left;
            }
            else
            {
                moveDir = Vector3.right;
            }

            RaycastHit hit;
            if(!rb.SweepTest(moveDir, out hit, swipeMove))
            {
                rb.MovePosition(rb.position + (moveDir * swipeMove));
            }

        }
    }

    /// <summary>
    /// figures out how to move the player horizontally
    /// </summary>
    /// <param name="pixelPosition">the posistion that's been touched/clicked </param> 
    /// <returns>the direction to move in tghe x axxis </returns>
    float CalculateMovement(Vector3 pixelPosition)
    {
        //convert this from screen to viewport, passing in the touch pos this time
        Vector3 worldPos = Camera.main.ScreenToViewportPoint(pixelPosition);

        float xMove = 0;
        //if we pressed the rigtht side of the screen
        if (worldPos.x > .5f)
        {
            xMove = -1;
            Debug.Log("clicked on the right side of the screen!");
        }
        //if we pressed the left side of the screen
        if (worldPos.x <= 0.5f)
        {
            xMove = 1;
            Debug.Log("Clicked on the left side of the screen!");
        }

        //replace horiz speed with new speed
        return (xMove * dodgeSpeed);

    }

    private void FixedUpdate()
    {
        //Apply the auto-moving and movement forces
        rb.AddForce(horizontalSpeed, 0, rollSpeed);
        forwardMovement = rb.velocity;
    }
}
