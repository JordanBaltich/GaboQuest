using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private float InvulTime;

    public bool invulnerable = false;
  
    IEnumerator invulRoutine;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void TakeDamage(int amount)
    {
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

    IEnumerator InvulnerableTimer()
    {
        invulnerable = true;
        yield return new WaitForSeconds(InvulTime);

        invulnerable = false;
        invulRoutine = null;
    }
}
