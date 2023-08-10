using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Cambia el valor máximo de la barra de salud.
    public void ChangeMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth; // Establece el valor máximo de la barra de salud.
    }

    // Cambia el valor actual de la barra de salud.
    public void ChangeCurrentHealth(float healthCant)
    {
        slider.value = healthCant; // Establece el valor actual de la barra de salud.
    }

    // Inicializa la barra de salud con un valor específico.
    public void StartHealthBar(float healthCant)
    {
        ChangeMaxHealth(healthCant); // Establece el valor máximo de salud.
        ChangeCurrentHealth(healthCant); // Establece el valor actual de salud.
    }
}
