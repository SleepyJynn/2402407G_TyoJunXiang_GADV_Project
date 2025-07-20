using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    // Start is called before the first frame update
    //setting up variables
    public float duration = 1f;

    private bool normalgrav = true;
    public bool isFlipped = false;
    private Coroutine currentFlip;

    public GameUIManager uiManager;
    // Update is called once per frame
    void Update()
    {
        GravSwitch();
    }

    void GravSwitch()
    {
        //if player presses gravity switch button
        if (Input.GetKeyDown(KeyCode.G))
        {
            //change gravity based on current gravity
            normalgrav = !normalgrav;
            this.GetComponent<Rigidbody2D>().gravityScale = normalgrav ? 1 : -1;
            //tell uiManager to update gravity mode text
            uiManager.SetGravity(normalgrav);
            //flip player sprite based on current gravity
            isFlipped = !isFlipped;
            this.GetComponent<SpriteRenderer>().flipX = isFlipped;
            //stop coroutine to prevent overlap
            if (currentFlip != null)
                StopCoroutine(currentFlip);
            //start coroutine to flip player sprite based on current sprite orientation
            currentFlip = StartCoroutine(CoroutineFlip(isFlipped ? 180f : 0f));
        }
    }
    //coroutine to flip player sprite based on current sprite orientation
    IEnumerator CoroutineFlip(float targetAngle)
    {
        //setting up variables
        float start = transform.eulerAngles.z;
        float timer = 0;

        while (timer < duration)
        {
            //increase timer based on time
            timer += Time.deltaTime;
            //find out how far into the animation is (0 to 1)
            float progress = timer / duration;
            //change angle of sprite from start to end over time
            float newAngle = Mathf.LerpAngle(start, targetAngle, progress);
            //honestly still not too sure of this but i think it rotates the player over time to match the target angle
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
            //wait until next frame
            yield return null;
        }

        //not too sure on this as well but it should make sure that i end at the target angle
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        //set currentFlip to null
        currentFlip = null;
    }

}
