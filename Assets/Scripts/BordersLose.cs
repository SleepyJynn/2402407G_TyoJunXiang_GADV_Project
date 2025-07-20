using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BordersLose : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI LoseText;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LoseText.gameObject.SetActive(true);
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
    }
}
