using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    [SerializeField]
    internal MovingWalls movingWall;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            destroythisObject();
        }
    }

    void destroythisObject()
    {
        print("destroying");
        movingWall.RemoveEnemy(gameObject);

        Destroy(gameObject);
    }
}
