using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 20;
    private int health;

    public Scrollbar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!healthBar)
            return;
        
        if (health == maxHealth)
        {
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
        }
        
        healthBar.size = (float)health / (float)maxHealth;
    }

    public void Hurt(int damages)
    {
        health -= damages;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
