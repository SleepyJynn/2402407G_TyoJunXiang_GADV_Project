using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration = 1f;

    private Coroutine currentFlip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //flip gravity
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale *= -1;

            //flip sprite visually
            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
            sr.flipX = !sr.flipX;

            //stop coroutine to prevent overlap
            if (currentFlip != null)
                StopCoroutine(currentFlip);

            //start coroutine to flip player sprite based on current sprite orientation
            float targetAngle = Mathf.Approximately(collision.transform.eulerAngles.z, 0f) ? 180f : 0f;
            currentFlip = StartCoroutine(CoroutineFlip(collision.transform, targetAngle));
        }
    }
    IEnumerator CoroutineFlip(Transform target, float targetAngle)
    {
        //setting up variables
        float start = target.eulerAngles.z;
        float timer = 0;

        while (timer < duration)
        {
            //increase timer based on time
            timer += Time.deltaTime;
            //find out how far into the animation is (0 to 1)
            float progress = timer / duration;
            //change angle of sprite from start to end over time
            float newAngle = Mathf.LerpAngle(start, targetAngle, progress);
            //rotate the player over time to match the target angle
            target.rotation = Quaternion.Euler(0, 0, newAngle);
            //wait until next frame
            yield return null;
        }

        //make sure to end exactly at the target angle
        target.rotation = Quaternion.Euler(0, 0, targetAngle);
        //set currentFlip to null
        currentFlip = null;
    }
}
