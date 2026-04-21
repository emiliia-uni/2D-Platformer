using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float health;
    private bool canReciveDemage =  true;
    public float invincibilityTimer = 2;

    public delegate void HealthChangeHandler(float newHealth, float amountChanged);
    public event HealthChangeHandler OnHealthChanged;

    public delegate void HealthInitializedHandler(float newHealth);
    public event HealthInitializedHandler OnHelthInitialized;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        OnHelthInitialized?.Invoke(health);
    }


// Update is called once per frame
void Update()
    {

    }

    public void AddDamage(float damage)
    {
        if (canReciveDemage)
        {
            health -= damage;
            OnHealthChanged?.Invoke(health,-damage);
            canReciveDemage = false;
            StartCoroutine(InvincibilityTimer(invincibilityTimer, ResetInvincibility));
        }
        
    }

    IEnumerator InvincibilityTimer(float time, Action callback)
    {
        yield return new WaitForSeconds(time);
        callback.Invoke();
    }

    private void ResetInvincibility()
    { 
        canReciveDemage = true;
        Debug.Log("reset");
    }

   public void AddHealth(float healthToAdd) 
    {
        health += healthToAdd;
        OnHealthChanged?.Invoke(health, healthToAdd);
        


    }




}
