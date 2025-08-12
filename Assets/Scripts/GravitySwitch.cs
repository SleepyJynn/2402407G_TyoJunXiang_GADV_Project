using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration = 1f;

    private bool normalgrav = true;
    public bool isFlipped = false;
    private Coroutine currentFlip;

    public GameUIManager uiManager;

    public AudioSource audioSource;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        CheckForInput();
    }

    void CheckForInput()
    {
        // Manual gravity flip with G key
        if (Input.GetKeyDown(KeyCode.G))
        {
            audioSource.Play();
            FlipGravity();
        }
    }

    public void FlipGravity()
    {
        // Change gravity state
        normalgrav = !normalgrav;
        this.GetComponent<Rigidbody2D>().gravityScale = normalgrav ? 1 : -1;

        // Update UI
        uiManager.SetGravity(normalgrav);

        // Flip sprite visual
        isFlipped = !isFlipped;
        this.GetComponent<SpriteRenderer>().flipX = isFlipped;

        // Stop old coroutine if running
        if (currentFlip != null)
            StopCoroutine(currentFlip);

        // Start flip animation
        currentFlip = StartCoroutine(CoroutineFlip(isFlipped ? 180f : 0f));
    }

    IEnumerator CoroutineFlip(float targetAngle)
    {
        float start = transform.eulerAngles.z;
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;
            float newAngle = Mathf.LerpAngle(start, targetAngle, progress);
            transform.rotation = Quaternion.Euler(0, 0, newAngle);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        currentFlip = null;
    }
}