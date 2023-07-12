using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;

    public Button defaultButton;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("player") == null)
        {
            gameOverPanel.SetActive(true);

            if (defaultButton != null)
            {
                defaultButton.Select();
            }

            Time.timeScale = 0f;
        }
    }

    public void Restart()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
