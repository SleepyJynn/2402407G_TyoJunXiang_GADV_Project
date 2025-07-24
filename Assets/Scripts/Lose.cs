using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI LoseText;
    public GameUIManager uiManager;

    private bool triggeredLoss = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForTimeLoss();
        CheckForRestart();
    }
    void CheckForTimeLoss()
    {
        if (uiManager.HasLost && triggeredLoss == false)
        {
            triggeredLoss = true;
            LoseText.gameObject.SetActive(true);
            StartCoroutine(FadeOutPlayer());
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LoseText.gameObject.SetActive(true);
            triggeredLoss = true;
            Debug.Log("You Died");
            StartCoroutine(FadeOutPlayer());
        }
    }
    IEnumerator FadeOutPlayer()
    {
        SpriteRenderer sr = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        Color playercolor = sr.color;

        Color loseTextColorValues = LoseText.color;
        loseTextColorValues.a = 0;
        LoseText.color = loseTextColorValues;

        while (loseTextColorValues.a < 1)
        {
            playercolor.a -= Time.deltaTime;
            loseTextColorValues.a += Time.deltaTime / 2.5f;
            LoseText.color = loseTextColorValues;
            sr.color = playercolor;
            yield return null;
        }
        Time.timeScale = 0f; //freeze time
    }

    void CheckForRestart()
    {
        if (triggeredLoss == true && Input.GetMouseButtonDown(0))  // Left click
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
