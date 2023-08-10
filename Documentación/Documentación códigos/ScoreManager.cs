using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    public float score;

    public PlayerController playerController; 


    void Update()
    {
        // Verifica si el objeto con la etiqueta "player" está presente en la escena
        if (GameObject.FindGameObjectWithTag("player") != null)
        {
            // Incrementa el puntaje con el tiempo
            score += 20 * Time.deltaTime;

            // Actualiza el texto del puntaje
            scoreText.text = ((int)score).ToString();

            // Si el puntaje es múltiplo de 1000, incrementa la salud del jugador
            if ((int)score % 1000 == 0 && (int)score != 0)
            {
                playerController.AddHealth();
            }
        }
    }
}