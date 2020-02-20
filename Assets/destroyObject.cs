using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    MovingWalls movingWall;

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
