﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Natalie Eidt
/// </summary>
public class UpgradeButtons : MonoBehaviour
{
    /// <summary>
    /// reference to the ghost powerup script
    /// </summary>
    public GhostPowerup ghostScript;

    /// <summary>
    /// reference to the double points scripts
    /// </summary>
    public DoublePointsEffect doublePointsScript;

    public void UpgradeGhostDuration()
    {
        ghostScript.ghostDuration += 2f;
    }

    public void UpgradeDoublePointsDuration()
    {
        doublePointsScript.duration += 2f;
    }

}
