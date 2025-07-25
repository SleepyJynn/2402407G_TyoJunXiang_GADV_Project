using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    //flash color value
    public Color FlashColor = Color.red;
    public float FlashDuration = 0.1f;
    //x amt of flashes before decay
    public int FlashCount = 5;

    private bool triggered = false;
    private SpriteRenderer sr;
    //stores platform original color for switching
    private Color originalColor;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.tag == "Player")
        {
            triggered = true;
            StartCoroutine(Decay());
        }
    }

    IEnumerator Decay()
    {
        //total time platform will flash
        float flashTime = FlashCount * FlashDuration * 2;
        float waitTime = 5f - flashTime;

        // Wait before starting flash (so platform lasts ~5s total)
        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < FlashCount; i++)
        {
            sr.color = FlashColor;
            yield return new WaitForSeconds(FlashDuration);
            sr.color = originalColor;
            yield return new WaitForSeconds(FlashDuration);
        }

        Destroy(gameObject);
    }
}
