using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthBarText;
    private float maxHealth;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        maxHealth = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
        healthBarText.text = "Health:    " + health + "/" + maxHealth;
    }
}
