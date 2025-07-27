using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI winText;

    private bool hasWon = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForRestart();
    }

    //when something enters the collider of the door
    void OnTriggerEnter2D(Collider2D collision)
    {
        //if collider has "Player" tag
        if (collision.gameObject.tag == "Player")
        {
            //set the winText to be active
            winText.gameObject.SetActive(true);
            Debug.Log("Game Won");
            hasWon = true; // track win state
            //start the coroutine to have the winText fade in
            StartCoroutine(FadeInWin());
        }
    }
    //coroutine to fade in winText
    IEnumerator FadeInWin()
    {
        //setting up the variable
        Color winTextColorValues = winText.color;
        //setting the alpha value to 0
        winTextColorValues.a = 0;
        winText.color = winTextColorValues;

        while (winTextColorValues.a < 1)
        {
            //increase alpha depending on time for the fade in effect
            winTextColorValues.a += Time.deltaTime / 2.5f;
            winText.color = winTextColorValues;
            yield return null;
        }
        Time.timeScale = 0.0f;
    }
    void CheckForRestart()
    {
        if (hasWon == true && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1.0f;
        }
    }
}
