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
    public bool HasLost = false;

    private float timer = 30f;
    private bool isGravityNormal = true;
    private bool isFlashing = false;

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
        timer -= Time.deltaTime;
        timerText.text = "Time Left: " + timer.ToString("F1") + "s";
        //update gravity mode text
        gravityText.text = "Gravity: " + (isGravityNormal ? "Normal" : "Inverse");
        //change color of gravity text depending on the mode
        gravityText.color = isGravityNormal ? Color.white : Color.red;

        //if time left is below 10 seconds start flashing red
        if (timer <= 10f && isFlashing == false)
        {
            StartCoroutine(FlashTimerText());
        }

        if (timer <= 0 && HasLost == false)
        {
            timer = 0;
            HasLost = true;
        }
    }

    IEnumerator FlashTimerText()
    {
        isFlashing = true;
        while (timer > 0 && timer <= 10f)
        {
            timerText.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            timerText.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }
        timerText.color = Color.white;
        isFlashing = false;
    }

    public void AddTime(float extraTime)
    {
        timer += extraTime;
    }
}
