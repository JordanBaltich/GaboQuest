using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int currentHealth;
    [SerializeField] private float InvulTime;

    [SerializeField] internal MovingWalls m_movingWall;
    Blackboard m_blackboard;

    public bool invulnerable = false;
  
    IEnumerator invulRoutine;

    private void Awake()
    {
        currentHealth = maxHealth;
        m_blackboard = GetComponent<Blackboard>();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

    }

    public void TakeDamage(int amount)
    {
        if(m_blackboard!= null)
        {
            m_blackboard.GetGameObjectVar("Target").Value = GameObject.FindGameObjectWithTag("Player");
        }

        if (!invulnerable)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            StartInvulTimer();
        }
    }

    void StartInvulTimer()
    {
        if (invulRoutine == null)
        {
            invulRoutine = InvulnerableTimer();
            StartCoroutine(invulRoutine);
        }
        else
        {
            StopCoroutine(invulRoutine);
            invulRoutine = InvulnerableTimer();
            StartCoroutine(invulRoutine);
        }
    }

    public void DestroyObject()
    {
        m_movingWall.RemoveEnemy(transform.parent.gameObject);
        Destroy(gameObject);    
    }

    IEnumerator InvulnerableTimer()
    {
        invulnerable = true;
        yield return new WaitForSeconds(InvulTime);

        invulnerable = false;
        invulRoutine = null;
    }
}
