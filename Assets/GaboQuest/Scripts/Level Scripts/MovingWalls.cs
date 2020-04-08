using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    public enum WallType
    {
        Pressure,
        Enemy
    }

    public GameObject startPos;
    public GameObject endPos;

    public List<GameObject> enemies;

    
    public WallType wallType;

    private void Start()
    {
        //AssignAllEnemiestoThisWall();
    }


    //void AssignAllEnemiestoThisWall()
    //{
    //    foreach(GameObject enemy in enemies)
    //    {
    //        enemyHealth.Add(enemy.GetComponentInChildren<Health>());
    //    }

    //    foreach (Health enemy in enemyHealth)
    //    {
    //        enemy.m_movingWall = this;
    //    }
    //}


    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    private void Update()
    {
        if(wallType == WallType.Enemy)
        {
            if (enemies.Count <= 0)
                moveDown();
            if (enemies.Count > 0)
                moveUp();
        }
    }

    internal void moveUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPos.transform.position, 5 * Time.deltaTime);
    }

    internal void moveDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, 5 * Time.deltaTime);
    }
}
