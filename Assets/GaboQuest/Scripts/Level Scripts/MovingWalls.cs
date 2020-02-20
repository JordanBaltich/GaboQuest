using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    public GameObject startPos;
    public GameObject endPos;

    public List<GameObject> enemies;

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        print(enemies.Count);

        
    }

    private void Update()
    {
        if (enemies.Count <= 0)
        {
            moveDown();
        }
    }

    void moveUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos.transform.position, 5 * Time.deltaTime);
    }

    void moveDown()
    {
        print("movingdown");
        transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, 5 * Time.deltaTime);
    }



}
