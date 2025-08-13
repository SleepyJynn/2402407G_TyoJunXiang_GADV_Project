using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    // Start is called before the first frame update

    // UI elements for displaying timer and gravity mode
    public TextMeshProUGUI TimerText;
    public TextMeshProUGUI GravityText;
    
    public bool HasLost = false; // flag to track if the player has lost

    private float timer = 30f; // internal timer value
    private bool isGravityNormal = true; // tracks whether gravity is normal or inverted
    private bool isFlashing = false; // prevents multiple coroutine instances for flashing

    private AudioSource audioSource; // audio source for timer warnings

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        /* check timer and gravity mode continuously
        to update UI and trigger events like flashing text or loss */
        CheckTimerAndGravity();
    }

    public void SetGravity(bool normal)
    {
        /* updates gravity mode so UI reflects current state */
        isGravityNormal = normal;
    }

    void CheckTimerAndGravity()
    {
        /* reduce timer every frame based on real time
        and update UI text accordingly */
        timer -= Time.deltaTime;
        TimerText.text = "Time Left: " + timer.ToString("F1") + "s";
        
        GravityText.text = "Gravity: " + (isGravityNormal ? "Normal" : "Inverse");
        GravityText.color = isGravityNormal ? Color.white : Color.red;

        /* start flashing timer and play warning sound when
        timer drops below 10 seconds. Prevents multiple flashes */
        if (timer <= 10f && isFlashing == false)
        {
            audioSource.Play();
            StartCoroutine(FlashTimerText());
        }

        /* when timer reaches 0, stop audio and mark player as lost */
        if (timer <= 0 && HasLost == false)
        {
            timer = 0;
            audioSource.mute = true;
            HasLost = true;
        }
    }

    IEnumerator FlashTimerText()
    {
        /* coroutine alternates timer text color to indicate urgency */
        isFlashing = true;
        while (timer > 0 && timer <= 10f)
        {
            TimerText.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            TimerText.color = Color.white;
            yield return new WaitForSeconds(0.3f);
        }
        TimerText.color = Color.white;
        isFlashing = false;
    }

    public void AddTime(float extraTime)
    {
        /* allows external scripts (like collectibles) to
        increase the player's timer */
        timer += extraTime;
    }
}
