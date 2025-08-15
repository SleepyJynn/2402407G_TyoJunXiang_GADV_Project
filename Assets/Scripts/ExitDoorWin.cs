using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitDoorWin : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI WinText; // the win text that appears when the player reaches the exit
    public Image Panel; // the panel background to fade in with the win text

    private bool hasWon = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* continuously check for input to allow restarting
        or returning to main menu after winning */
        CheckForInput();
    }

    // triggered when something enters the collider of the exit door
    void OnTriggerEnter2D(Collider2D collision)
    {
        /* only trigger win sequence if the collider belongs to the Player
        activate the text and panel mark win state then start fade coroutine */
        if (collision.gameObject.tag == "Player")
        {
            WinText.gameObject.SetActive(true);
            Panel.gameObject.SetActive(true);
            
            hasWon = true;

            StartCoroutine(FadeInWinFadeInPanel());
        }
    }

    // coroutine to fade in the win text and panel and to better control it
    IEnumerator FadeInWinFadeInPanel()
    {
        // setup wintext fade
        Color winTextColorValues = WinText.color;
        winTextColorValues.a = 0;
        WinText.color = winTextColorValues;

        // setup panel fade
        Color panelColorValues = Panel.color;
        panelColorValues.a = 0;
        Panel.color = panelColorValues;

        // fade in loop
        while (winTextColorValues.a < 1)
        {
            // gradually increase alpha values over time
            winTextColorValues.a += Time.deltaTime / 1.5f; // fade duration for text
            WinText.color = winTextColorValues;

            panelColorValues.a += Time.deltaTime; // fade duration for panel
            Panel.color = panelColorValues;

            yield return null;
        }
        // stop game time once fully faded
        Time.timeScale = 0.0f;
    }
    void CheckForInput()
    {
        /* allow player to restart the level with left click
        or return to main menu with Escape key after winning */
        if (hasWon == true && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1.0f;
        }
        if (hasWon == true && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            Time.timeScale = 1.0f;
        }
    }
}
