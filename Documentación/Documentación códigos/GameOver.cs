using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel; // El panel que se muestra cuando el juego ha terminado

    public Button defaultButton; // El botón que se selecciona por defecto cuando se muestra el panel de Game Over

    void Update()
    {
        // Verifica si el objeto con la etiqueta "player" no está presente en la escena
        if (GameObject.FindGameObjectWithTag("player") == null)
        {
            gameOverPanel.SetActive(true); // Activa el panel de Game Over

            if (defaultButton != null)
            {
                defaultButton.Select();
            }

            Time.timeScale = 0f; // Pausa el tiempo en el juego
        }
    }

    // Reinicia la escena del juego
    public void Restart()
    {
        Time.timeScale = 1f; // Reanuda el tiempo en el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Carga la escena actual nuevamente
    }
}