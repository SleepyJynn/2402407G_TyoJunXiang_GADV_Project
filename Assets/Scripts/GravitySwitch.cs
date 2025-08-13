using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitch : MonoBehaviour
{
    // Start is called before the first frame update

    public float Duration = 1f; // duration of the sprite flip animation
    public bool IsFlipped = false; // tracks whether the player's sprite is visually flipped
    public GameUIManager UiManager; // reference to the UI manager to update gravity display
    public ParticleSystem GravityFlipFX; // particle system to play when gravity flips

    private bool normalgrav = true; // tracks whether gravity is normal (true) or inverted (false)
    private AudioSource audioSource; // audio source to play flip sound
    private Coroutine currentFlip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        /* check for player input each frame for manual gravity flips */
        CheckForInput();
    }

    void CheckForInput()
    {
        /* if player presses 'G' flip gravity play audio and spawn particles
        this separates input handling from the actual flip logic */
        if (Input.GetKeyDown(KeyCode.G))
            if (Input.GetKeyDown(KeyCode.G))
        {
            audioSource.Play();
            GravityFlipFX.Play();
            FlipGravity();
        }
    }

    public void FlipGravity()
    {
        /* toggle gravity scale between 1 (normal) and -1 (inverted)
        also updates the UI to reflect the current gravity state */
        normalgrav = !normalgrav;
        this.GetComponent<Rigidbody2D>().gravityScale = normalgrav ? 1 : -1;

        UiManager.SetGravity(normalgrav);

        /* flip the player's sprite visually
        IsFlipped controls the flipX property for immediate visual feedback */
        IsFlipped = !IsFlipped;
        this.GetComponent<SpriteRenderer>().flipX = IsFlipped;

        /* stop any running flip animation to prevent overlap
        ensures smooth rotation even if the player flips rapidly */
        if (currentFlip != null)
            StopCoroutine(currentFlip);

        // start coroutine to smoothly rotate the sprite based on gravity state
        currentFlip = StartCoroutine(CoroutineFlip(IsFlipped ? 180f : 0f));
    }

    IEnumerator CoroutineFlip(float targetAngle)
    {
        // store initial rotation
        float start = transform.eulerAngles.z;
        float timer = 0;

        /* smoothly rotate sprite over Duration seconds
        LerpAngle handles rotation across 0/360 degrees correctly */
        while (timer < Duration)
        {
            timer += Time.deltaTime;

            float progress = timer / Duration;
            float newAngle = Mathf.LerpAngle(start, targetAngle, progress);

            transform.rotation = Quaternion.Euler(0, 0, newAngle);

            yield return null;
        }

        // ensure exact final rotation
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);

        // clear coroutine reference
        currentFlip = null;
    }
}