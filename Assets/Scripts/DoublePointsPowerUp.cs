using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * By Brandon Laing (JCCC)
 * This will check if they player runs into a double points powerup and added the effect to them.
 */

public class DoublePointsPowerUp : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.GetComponent<PlayerScore>() != null)
    {
      other.gameObject.AddComponent<DoublePointsEffect>();
      Destroy(this.gameObject);
    }
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.GetComponent<PlayerScore>() != null)
    {
      collision.gameObject.AddComponent<DoublePointsEffect>();
      Destroy(this.gameObject);
    }
  }
}
