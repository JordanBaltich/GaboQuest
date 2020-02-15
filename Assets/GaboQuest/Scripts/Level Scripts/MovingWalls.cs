using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{

    public GameObject startPos;
    public GameObject endPos;

    public void moveUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos.transform.position, 5 * Time.deltaTime);
    }

    public void moveDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, 5 * Time.deltaTime);
    }
}
