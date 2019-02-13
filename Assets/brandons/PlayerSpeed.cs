using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    public static PlayerSpeed main;
    private float _playerSpeed = 5;
    public float playerSpeedIncreasePerSecond = 5F;
    public float maxPlayerSpeed = 25;
    public float PlayerSpeedProperty
    {
        get { return _playerSpeed; }
        set
        {
            if (value > maxPlayerSpeed)
                _playerSpeed = maxPlayerSpeed;
            else
                _playerSpeed = value;
        }
    }

    public float playerSpeedMirror;

    private void Start()
    {
        main = this;
    }

    private void FixedUpdate()
    {
        PlayerSpeedProperty += playerSpeedIncreasePerSecond * Time.fixedDeltaTime;
        playerSpeedMirror = PlayerSpeedProperty;
    }
}