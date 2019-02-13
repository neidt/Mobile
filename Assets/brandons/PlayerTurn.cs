using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    public static PlayerTurn main;

    public List<Transform> leftColliders = new List<Transform>();
    public bool CanTurnLeft
    {
        get
        {
            return leftColliders.Count > 0;
        }
    }

    public List<Transform> rightColliders = new List<Transform>();
    public bool CanTurnRight
    {
        get
        {
            return rightColliders.Count > 0;
        }
    }

    private void Start()
    {
        main = this;
    }

    public Vector3 touchStart;
    public float minPixleDifference = 10;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                touchStart = Input.touches[0].position;
                return;
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                float distance = Input.touches[0].position.x - touchStart.x;
                distance = Mathf.Abs(distance);

                if (distance < minPixleDifference)
                    return;

                if (touchStart.x > Input.touches[0].position.x)
                {
                    if (CanTurnLeft)
                    {
                        UpdateDirection(Vector3.left);
                        Camera.main.GetComponentInParent<PlayerCamera>().UpdateRotation(Vector3.left);
                        transform.eulerAngles += new Vector3(0, -90, 0);
                    }
                    else
                        Die.PlayerDie();
                }
                else if (touchStart.x < Input.touches[0].position.x)
                {
                    if (CanTurnRight)
                    {
                        UpdateDirection(Vector3.right);
                        Camera.main.GetComponentInParent<PlayerCamera>().UpdateRotation(Vector3.right);
                        transform.eulerAngles += new Vector3(0, 90, 0);
                    }
                    else
                        Die.PlayerDie();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            UpdateDirection(Vector3.left);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            UpdateDirection(Vector3.right);
    }

    public Vector3 moveDirection = Vector3.forward;

    private void UpdateDirection(Vector3 direction)
    {
        // update the move direction
        if (moveDirection == Vector3.forward)
        {
            moveDirection = direction;
            return;
        }

        if (moveDirection == Vector3.left)
        {
            if (direction == Vector3.left)
                moveDirection = Vector3.back;
            else
                moveDirection = Vector3.forward;
            return;
        }

        if (moveDirection == Vector3.right)
        {
            if (direction == Vector3.left)
                moveDirection = Vector3.forward;
            else
                moveDirection = Vector3.back;
            return;
        }

        if (moveDirection == Vector3.back)
        {
            if (direction == Vector3.left)
                moveDirection = Vector3.right;
            else
                moveDirection = Vector3.left;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RightTurn") && !rightColliders.Contains(other.transform))
        {
            rightColliders.Add(other.transform);
        }
        else if (other.CompareTag("LeftTurn") && !leftColliders.Contains(other.transform))
        {
            leftColliders.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RightTurn") && rightColliders.Contains(other.transform))
        {
            rightColliders.Remove(other.transform);
        }
        else if (other.CompareTag("LeftTurn") && leftColliders.Contains(other.transform))
        {
            leftColliders.Remove(other.transform);
        }
    }
}
