using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinUIController : MonoBehaviour
{
    public TextMeshProUGUI winText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
