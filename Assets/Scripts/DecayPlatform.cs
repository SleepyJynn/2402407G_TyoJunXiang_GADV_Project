using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    public Color FlashColor = Color.red; // the color the platform flashes before disappearing
    public float FlashDuration = 0.1f; // how long the platform stays in FlashColor each time it flashes
    public int FlashCount = 5; // number of flashes before the platform is destroyed

    private bool triggered = false; // tracks if the platform has already been stepped on by the player
    private SpriteRenderer sr; // reference to the SpriteRenderer to change the platform's color
    private Color originalColor; // the platform's original color to restore after flashing
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
        /* if the player touches the platform for the first time
        mark it as triggered and start the decay sequence */
        if (!triggered && collision.gameObject.tag == "Player")
        {
            triggered = true;
            StartCoroutine(Decay());
        }
    }

    //used coroutines to better control decay
    IEnumerator Decay()
    {
        /* wait a few seconds before flashing the platform
        flash it several times before destroying it */

        float flashTime = FlashCount * FlashDuration * 2; // calculate total time spent flashing
        float waitTime = 5f - flashTime; // make the platform last ~5 seconds total before disappearing

        // wait before starting flash (so platform lasts ~5s total)
        yield return new WaitForSeconds(waitTime);

        // flash between FlashColor and originalColor
        for (int i = 0; i < FlashCount; i++)
        {
            sr.color = FlashColor;
            yield return new WaitForSeconds(FlashDuration);
            sr.color = originalColor;
            yield return new WaitForSeconds(FlashDuration);
        }

        // finally destroy the platform
        Destroy(gameObject);
    }
}
