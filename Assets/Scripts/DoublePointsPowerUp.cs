using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointsPowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerScore>() != null)
        {
            other.gameObject.AddComponent<DoublePointsEffect>();
        }
    }
}
