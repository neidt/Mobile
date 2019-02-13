using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoublePointsEffect : MonoBehaviour
{
    public float duration = 5;
    PlayerScore myScore;

    private void Start()
    {
        myScore = GetComponent<PlayerScore>();
        myScore.modifier *= 2;
        Destroy(this, duration);
    }

    private void OnDestroy()
    {
        myScore.modifier /= 2;
    }
}
