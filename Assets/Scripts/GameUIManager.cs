using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gravityText;

    float timer = 0f;
    bool isGravityNormal = true;

    void Update()
    {
        CheckTimerAndGravity();
    }

    public void SetGravity(bool normal)
    {
        isGravityNormal = normal;
    }

    void CheckTimerAndGravity()
    {
        // Update timer
        timer += Time.deltaTime;
        timerText.text = "Time: " + timer.ToString("F1") + "s";

        // Update gravity status text
        gravityText.text = "Gravity: " + (isGravityNormal ? "Normal" : "Inverse");

        // Optional: Change color when gravity is inverse
        gravityText.color = isGravityNormal ? Color.white : Color.red;
    }
}
