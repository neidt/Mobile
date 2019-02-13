using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour
{
  public Transform leftPoint, middlePoint, rightPoint;
  public GameObject ghostPrefab;
  public GameObject doublePointsPrefab;

  private void Start()
  {
    if (leftPoint == null && middlePoint == null && rightPoint == null)
      return;

    int randomRange = Random.Range(0, 100);
    if (randomRange < 15)
    {
      if (randomRange < 5)
      {
        if (randomRange % 2 == 0)
        {
          Instantiate(ghostPrefab, leftPoint.position, Quaternion.identity, this.transform);
        }
        else
        {
          Instantiate(doublePointsPrefab, leftPoint.position, Quaternion.identity, this.transform);
        }
        return;
      }

      if (randomRange < 10)
      {
        if (randomRange % 2 == 0)
        {
          Instantiate(ghostPrefab, middlePoint.position, Quaternion.identity, this.transform);
        }
        else
        {
          Instantiate(doublePointsPrefab, middlePoint.position, Quaternion.identity, this.transform);
        }
        return;
      }

      if (randomRange < 15)
      {
        if (randomRange % 2 == 0)
        {
          Instantiate(ghostPrefab, rightPoint.position, Quaternion.identity, this.transform);
        }
        else
        {
          Instantiate(doublePointsPrefab, rightPoint.position, Quaternion.identity, this.transform);
        }
        return;
      }
    }
  }

  private void FixedUpdate()
    {
      transform.position -= PlayerTurn.main.moveDirection * PlayerSpeed.main.PlayerSpeedProperty * Time.fixedDeltaTime;
    }
}
