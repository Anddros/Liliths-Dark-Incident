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

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectWithTag("player") != null)
        {
            score += 20 * Time.deltaTime;
            scoreText.text = ((int)score).ToString();

            if ((int)score % 1000 == 0 && (int)score != 0)
            {
                playerController.AddHealth();
            }
        }


    }


}
