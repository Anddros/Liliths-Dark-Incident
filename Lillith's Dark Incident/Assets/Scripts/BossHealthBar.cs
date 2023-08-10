using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void ChangeCurrentHealth(float healthCant)
    {
        slider.value = healthCant;
    }

    public void StartHealthBar(float healthCant)
    {
        ChangeMaxHealth(healthCant);
        ChangeCurrentHealth(healthCant);
    }
}