using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    //setting up variables
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gravityText;

    private float timer = 0f;
    private bool isGravityNormal = true;

    // Update is called once per frame
    void Update()
    {
        CheckTimerAndGravity();
    }

    public void SetGravity(bool normal)
    {
        //setting isGravityNormal to whatever bool value normal is
        isGravityNormal = normal;
    }

    void CheckTimerAndGravity()
    {
        //update timer based on time
        timer += Time.deltaTime;
        timerText.text = "Time: " + timer.ToString("F1") + "s";
        //update gravity mode text
        gravityText.text = "Gravity: " + (isGravityNormal ? "Normal" : "Inverse");
        //change color of gravity text depending on the mode
        gravityText.color = isGravityNormal ? Color.white : Color.red;
    }
}
