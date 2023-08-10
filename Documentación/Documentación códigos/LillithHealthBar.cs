using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LillithHealthBar : MonoBehaviour
{
	private Slider slider; 

	void Start()
	{
		slider = GetComponent<Slider>(); 
	}

	public void ChangeMaxHealth(float maxHealth)
	{
		slider.maxValue = maxHealth; // Establece el valor máximo de la barra de vida
	}
	
	public void ChangeCurrentHealth(float healthCant)
	{
		slider.value = healthCant; // Establece el valor actual de la barra de vida
	}


	// Inicializa la barra de vida
	public void StartHealthBar(float healthCant)
	{
		ChangeMaxHealth(healthCant);
		ChangeCurrentHealth(healthCant);
	}
}
