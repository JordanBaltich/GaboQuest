using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Locker : MonoBehaviour
{
    public MovingWalls[] m_movingWalls;
    
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private List<Health> enemyHealth;

    public int playerID;

    bool room_cleared;

    private void Start()
    {
        AssignAllEnemiestoThisRoom();
    }

    public void RemoveEnemy(GameObject enemy)
    {
        foreach (MovingWalls m_wall in m_movingWalls)
        {
            m_wall.RemoveEnemy(enemy);
        }
        enemies.Remove(enemy);
    }

    void AssignAllEnemiestoThisRoom()
    {
        foreach (GameObject enemy in enemies)
        {
            enemyHealth.Add(enemy.GetComponentInChildren<Health>());
        }

        foreach (Health enemy in enemyHealth)
        {
            enemy.m_roomLocker = this;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerID)
        {
            foreach (MovingWalls m_wall in m_movingWalls)
            {
                m_wall.enemies.Clear();
                foreach (GameObject enemy in enemies)
                {
                    m_wall.enemies.Add(enemy);
                }
            }
        }
    }
}
