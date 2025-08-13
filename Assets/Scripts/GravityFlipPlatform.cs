using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource audioSource; // AudioSource component to play sound when player touches the platform
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        /* only respond to collisions with objects tagged "Player"
        calls the FlipGravity method from the GravitySwitch script attached
        to the player ensuring the platform interacts with the player's gravity state */
        audioSource.Play();
        if (collision.gameObject.CompareTag("Player"))
        {
            GravitySwitch gs = collision.gameObject.GetComponent<GravitySwitch>();
            gs.FlipGravity();
        }
    }
}
