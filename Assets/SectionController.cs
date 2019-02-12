using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SectionType
{
  Forward, LeftTurn, RightTurn
}

public class SectionController : MonoBehaviour
{
  public SectionType sectionType;
  public float moveSpeed;

  private void FixedUpdate()
  {
    transform.position -= Vector3.forward * moveSpeed * Time.fixedDeltaTime;
  }
}
