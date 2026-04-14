using System;
using TMPro;
using UnityEngine;

public class UiHelthDisplay : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth.OnHealthChanged += OnHealthChange;
        playerHealth.OnHelthInitialized += OnHelthInitialized;
    }

    private void OnHelthInitialized(float newHealth)
    {
        healthText.text = newHealth.ToString();
    }

    public void OnHealthChange(float newHealth, float amountChanged) 
    {
        //Debug.Log("On Health Change Event");
        healthText.text = newHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
