using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position -= PlayerTurn.main.moveDirection * PlayerSpeed.main.PlayerSpeedProperty * Time.fixedDeltaTime;
    }
}
