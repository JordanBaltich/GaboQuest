using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    public int currentHealth;
    [SerializeField] private float InvulTime;

    [SerializeField] internal Room_Locker m_roomLocker;
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

    /// <summary>
    /// tests for killing enemies to open gates
    /// </summary>
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        if(m_roomLocker != null)
    //            DestroyObject();
    //    }
    //}

    //removes reference from roomlocker to this object so the gate unlocks
    public void DestroyObject()
    { 
        
        if (m_roomLocker != null)
            m_roomLocker.RemoveEnemy(transform.parent.gameObject);

        Destroy(gameObject);    
    }

    //initiates invulnerable timer for when the target will be next vulnerable
    IEnumerator InvulnerableTimer()
    {
        invulnerable = true;
        yield return new WaitForSeconds(InvulTime);

        invulnerable = false;
        invulRoutine = null;
    }
}
