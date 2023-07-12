using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectVictory : MonoBehaviour
{


    public Button defaultButton;

    // Start is called before the first frame update
    void Start()
    {
        if (defaultButton != null)
            {
                defaultButton.Select();
            }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
