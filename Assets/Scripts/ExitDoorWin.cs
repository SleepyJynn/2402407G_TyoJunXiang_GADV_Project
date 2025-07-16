using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            winText.gameObject.SetActive(true);
            Debug.Log("Game Won");
            StartCoroutine(FadeInWin());
        }
    }
    IEnumerator FadeInWin()
    {
        Color winTextColorValues = winText.color;
        winTextColorValues.a = 0;
        winText.color = winTextColorValues;

        while (winTextColorValues.a < 1)
        {
            winTextColorValues.a += Time.deltaTime / 2.5f;
            winText.color = winTextColorValues;
            yield return null;
        }
    }
}
