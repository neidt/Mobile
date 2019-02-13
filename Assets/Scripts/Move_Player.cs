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
    Vector3 moveDir;
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
    /// animator stuff
    /// </summary>
    Animator anim;
    bool isGrounded = true;
    bool isCrouching = false;
    bool isJumping = false;
    int jumpHash = Animator.StringToHash("isJumping");
    int crouchHash = Animator.StringToHash("isCrouching");
    int runHash = Animator.StringToHash("isRunning");

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
    /// initialize components
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        //anim.SetBool(runHash, true);
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
        if (inputType == INPUT_TYPE.Accelerometer)
        {
            //move based on accelerometer
            //Debug.Log(Input.acceleration.x.ToString());
            horizontalSpeed = Input.acceleration.x * dodgeSpeed * Time.deltaTime;
        }

        //use the real android touch system
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.touches[0];

            //check for jumping or crouching
            SwipeJumpOrCrouch(firstTouch);
        }
#endif
    }

    /// <summary>
    /// detect touches and either jump or crouch based on the direction of swipes
    /// </summary>
    /// <param name="touch"> the touch that will be used </param>
    void SwipeJumpOrCrouch(Touch touch)
    {
        //check if touch has just started
        if (touch.phase == TouchPhase.Began)
        {
            touchStart = touch.position;
        }

        //check if touch was just ended
        else if (touch.phase == TouchPhase.Ended)
        {
            //get the position we ended the touch
            Vector2 touchEnd = touch.position;

            //calculate difference btwn start and end of touch
            float yDiff = touchEnd.y - touchStart.y;

            if (Mathf.Abs(yDiff) < minSwipeDistance)
            {
                return;
            }

            //check if the player is on the ground
            CheckIfGrounded(rb.position);

            if (yDiff < 0 && isGrounded && !isCrouching)//crouching
            {
                StartCoroutine(CrouchDown());
            }
            else if (yDiff > 0 && isGrounded && !isJumping)//jumping
            {
                StartCoroutine(JumpUp());
            }

            RaycastHit hit;
            if (!rb.SweepTest(moveDir, out hit, swipeMove))
            {
                StartCoroutine(ResetAnimationState());
            }
        }
    }

    /// <summary>
    /// sets the character to crouching
    /// </summary>
    /// <returns> waits for 2 seconds </returns>
    IEnumerator CrouchDown()
    {
        anim.SetBool(runHash, false);
        anim.SetBool(crouchHash, true);

        this.gameObject.GetComponent<CapsuleCollider>().height -= .6f;
        this.gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0, .6f, 0);
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<CapsuleCollider>().height += .6f;
        this.gameObject.GetComponent<CapsuleCollider>().center = new Vector3(0, .8f, 0);
    }

    /// <summary>
    /// sets the character to jumping
    /// </summary>
    /// <returns> waits for 2 seconds </returns>
    IEnumerator JumpUp()
    {
        anim.SetBool(runHash, false);
        anim.SetBool(jumpHash, true);
        isGrounded = false;
        yield return new WaitForSeconds(2f);
    }

    /// <summary>
    /// resets the animator variables
    /// </summary>
    /// <returns></returns>
    IEnumerator ResetAnimationState()
    {
        yield return new WaitForSeconds(1f);
        
        anim.SetBool(runHash, true);
        anim.SetBool(jumpHash, false);
        anim.SetBool(crouchHash, false);
        
    }

    /// <summary>
    /// check if the character is on the ground
    /// </summary>
    /// <param name="position"> the current player position </param>
    private void CheckIfGrounded(Vector3 position)
    {
        if (rb.position.y < 0f)
        {
            isGrounded = true;
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
        if (worldPos.y > .5f)
        {
            Debug.Log("clicked on the top side of the screen!");
        }
        //if we pressed the left side of the screen
        if (worldPos.y <= 0.5f)
        {
            Debug.Log("Clicked on the bottom side of the screen!");
        }

        //replace horiz speed with new speed
        return (xMove * dodgeSpeed);

    }

    private void FixedUpdate()
    {
        //Apply the auto-moving and movement forces
        rb.AddForce(horizontalSpeed, 0, 0);
        forwardMovement = rb.velocity;
    }
}
