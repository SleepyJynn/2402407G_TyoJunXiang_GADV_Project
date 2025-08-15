using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lose : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI LoseText; // text displayed on losing
    public GameUIManager UiManager; // reference to the UI manager (tracks HasLost)
    public Image Panel; // panel to fade in

    private bool triggeredLoss = false; // ensures loss logic only triggers once
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /* check if player lost by timer or trigger collision and input for restart */
        CheckForTimeLoss();
        CheckForInput();
    }

    //seperated timeloss and loss from out of bounds (oob) because they work slightly differently in the sense where timeloss needs to check variables while oob only checks for collision
    void CheckForTimeLoss()
    {
        // if the UI manager has flagged a loss and we haven't triggered it yet
        if (UiManager.HasLost && triggeredLoss == false)
        {
            triggeredLoss = true;

            LoseText.gameObject.SetActive(true);
            Panel.gameObject.SetActive(true);

            GameUIManager uiManager = FindObjectOfType<GameUIManager>();
            uiManager.HasLost = true;

            StartCoroutine(FadeOutPlayerFadeInLose());
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // if player collides with this object and loss hasn't been triggered trigger loss
        if (collision.gameObject.tag == "Player")
        {
            triggeredLoss = true;

            LoseText.gameObject.SetActive(true);
            Panel.gameObject.SetActive(true);

            GameUIManager uiManager = FindObjectOfType<GameUIManager>();
            uiManager.HasLost = true;

            StartCoroutine(FadeOutPlayerFadeInLose());
        }
    }

    //used coroutine to better control alpha values for fading in and out
    IEnumerator FadeOutPlayerFadeInLose()
    {
        // get the player sprite and store its current color
        SpriteRenderer sr = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        Color playercolor = sr.color;

        // prepare lose text to fade in
        Color loseTextColorValues = LoseText.color;
        loseTextColorValues.a = 0;
        LoseText.color = loseTextColorValues;

        // prepare panel to fade in
        Color panelColorValues = Panel.color;
        panelColorValues.a = 0;
        Panel.color = panelColorValues;

        // gradually fade out the player sprite while fading in UI elements
        while (loseTextColorValues.a < 1)
        {
            playercolor.a -= Time.deltaTime;
            sr.color = playercolor;

            loseTextColorValues.a += Time.deltaTime / 1.5f;
            LoseText.color = loseTextColorValues;

            panelColorValues.a += Time.deltaTime;
            Panel.color = panelColorValues;

            yield return null;
        }

        // stop time after the fade effect completes
        Time.timeScale = 0f;
    }

    void CheckForInput()
    {
        // restart the scene on left mouse click if loss has been triggered
        if (triggeredLoss == true && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // return to main menu on pressing Escape if loss has been triggered
        if (triggeredLoss == true && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
