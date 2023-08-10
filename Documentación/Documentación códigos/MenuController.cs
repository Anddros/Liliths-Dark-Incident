using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private TextMeshProUGUI titleText;
    private string title; // Título actual del juego
    private int easterCount = 0; 
    private int easterNum; 

    [Header("Load Levels")]
    public string newGameLevel; 
    private string levelToLoad; 
    [SerializeField] private GameObject NoLoadGame = null; 

    private int LimitFPS = 60; // Límite de fps

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor
        easterCount = Random.Range(0, 15); 
        easterNum = Random.Range(0, 15);
        Application.targetFrameRate = LimitFPS; // Establece el límite de fps
    }

    void Update()
    {
        titleText.SetText(title); // Actualiza el texto del título

        // Verifica si se debe activar el easter egg
        if (easterCount == easterNum)
        {
            title = "Lillith's Darky Incident"; // Establece el título alternativo
        }
        else
        {
            title = "Lillith's Dark Incident"; // Establece el título predeterminado
        }
    }

    public void NewGameYes()
    {
        SceneManager.LoadScene(newGameLevel, LoadSceneMode.Single); 
    }

    public void LoadGameYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel"); 
            SceneManager.LoadScene(levelToLoad); 
        }
        else
        {
            NoLoadGame.SetActive(true); // No hay partida guardada
        }
    }

    public void ExitButton()
    {
        Application.Quit(); // Cierra el juego
    }

    public void EasterTitle()
    {
        if (easterCount == 15)
        {
            easterCount = 0;
        }
        easterCount++;
    }
}