using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float RotationSpeed = 50f; // how fast the collectible rotates in degrees per second

    private float timeBonus = 5f; // amount of extra time given to the player when collected
    private int rotationDirection; // 1 for clockwise rotation -1 for counterclockwise rotation
    private AudioSource audioSource; // plays a sound effect when the collectible is picked up

    void Start()
    {
        ChooseRotation();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SpinSprite();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /* when another object enters this collectible's trigger
        play the pickup sound if the colliding object has the "Player" tag
        find the GameUIManager and call AddTime to increase the remaining time
        then destroy the collectible after the sound finishes playing */
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (collision.CompareTag("Player"))
        {
            GameUIManager uiManager = FindObjectOfType<GameUIManager>();
            if (uiManager != null)
            {
                uiManager.AddTime(timeBonus);
            }

            Destroy(gameObject, audioSource != null ? audioSource.clip.length : 0f);
        }
    }

    void ChooseRotation()
    {
        /* randomly choose whether this collectible will rotate
        clockwise or counterclockwise */
        int choice = Random.Range(1, 3);
        rotationDirection = (choice == 1) ? 1 : -1;
    }

    void SpinSprite()
    {
        /* continuously rotate the collectible's transform
        based on RotationSpeed and rotationDirection */
        transform.Rotate(0f, 0f, RotationSpeed * rotationDirection * Time.deltaTime);
    }
}
