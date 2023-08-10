using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public Button defaultButton;
    
    private void Update()
    {
        // Abrir menú 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true); // Activa el menú
            Time.timeScale = 0f; // Pausa el tiempo

            if (defaultButton != null)
            {
                defaultButton.Select();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false); // Desactiva el menú
        Time.timeScale = 1f; // Reanuda el tiempo
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        SceneManager.LoadScene(sceneID); // Carga la escena principal
    }
}