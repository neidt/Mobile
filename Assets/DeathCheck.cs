using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.transform.CompareTag("Player"))
    {
      Die.PlayerDie();
    }
  }
}
