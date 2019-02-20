using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Natalie Eidt
/// </summary>
public class UpgradeButtons : MonoBehaviour
{
    public void UpgradeGhostDuration()
    {
        GhostPowerup.ghostUpgradeDuration += 2f;
    }

    public void UpgradeDoublePointsDuration()
    {
       DoublePointsEffect.upgradeDuration += 2f;
    }

}
