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
    private string title;
    private int easterCount = 0;
    private int easterNum;

    [Header("Load Levels")]
    public string newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject NoLoadGame = null;

    private int LimitFPS = 60;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        easterCount = Random.Range(0, 15);
        easterNum = Random.Range(0, 15);
        Application.targetFrameRate = LimitFPS;
    }

    void Update()
    {
        titleText.SetText(title);
        if (easterCount == easterNum)
        {
            title = "Lillith's Darky Incident";
        }
        else
        {
            title = "Lillith's Dark Incident";
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
            NoLoadGame.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
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
