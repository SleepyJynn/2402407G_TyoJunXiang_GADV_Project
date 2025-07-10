using System.Collections;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    public float duration = 1f;

    private bool normalgrav = true;
    public bool isFlipped = false;
    private Coroutine currentFlip;

    public GameUIManager uiManager;
    void Update()
    {
        GravSwitch();
    }

    void GravSwitch()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Flip gravity
            normalgrav = !normalgrav;
            this.GetComponent<Rigidbody2D>().gravityScale = normalgrav ? 1 : -1;
            uiManager.SetGravity(normalgrav);

            // Flip sprite
            isFlipped = !isFlipped;
            this.GetComponent<SpriteRenderer>().flipX = true;

            if (currentFlip != null)
                StopCoroutine(currentFlip);

            currentFlip = StartCoroutine(CoroutineFlip(isFlipped ? 180f : 0f));
        }
    }
    IEnumerator CoroutineFlip(float targetAngle)
    {
        float start = transform.eulerAngles.z;
        float timer = 0;

        while (timer < duration)
        {
            // Move the timer forward
            timer += Time.deltaTime;

            // Calculate how far we are in the animation (0 to 1)
            float progress = timer / duration;

            // Slowly change the angle from start to target
            float newAngle = Mathf.LerpAngle(start, targetAngle, progress);

            // Apply the new rotation to the object
            transform.rotation = Quaternion.Euler(0, 0, newAngle);

            // Wait until next frame
            yield return null;
        }

        // Make sure it ends exactly at the target angle
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        currentFlip = null;
    }

}
