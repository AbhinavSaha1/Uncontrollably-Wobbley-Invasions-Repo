using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {
    
    public static HealthController instance;                                        //Creating a HealthController singleton

    public delegate void OnHealthChanged (float previousHealth, float health);      //Event and delegate for HealthController     
    public event OnHealthChanged onHealthChanged = delegate {};

    //Variables
    public float maxHealth = 100f;
    public float health;
    bool healthCanChange;

    void Awake() {
        if (instance == null) instance = this;                                     //Making sure there's only 1 object of this instance
        health = maxHealth;
    }

    public void TakeDamage(float damage) {
        healthCanChange = true;
        float oldHealth = health;

        if (healthCanChange) {
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);
            onHealthChanged(oldHealth, health);
        }
    }
}


/*
    Note: 1. For this script to work, you need a HealthChanged() function with parameters previousHealth and health as floats in the script on that object.
          2. You also need OnEnable() and OnDisable() function in that same script. No parameters.
          3. Create a HealthController variable like so, "HealthController health"
          4. In the Awake() function, do: health = GetComponent<HealthController>();
*/
