using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    public float RotationSpeed = 50f; // speed in degrees per second at which the asteroid will rotate
    
    private int rotationDirection; // rotation direction: 1 for clockwise -1 for counterclockwise
    private AudioSource audioSource; // reference to the asteroid AudioSource component

    void Start()
    {
        /* randomly choose if asteroid will rotate clockwise or counterclockwise 
        when game starts by calling ChooseRotation() 
        then get the AudioSource so it can be used later to play sound effects 
        without having to keep searching for it */
        ChooseRotation();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /* continuously rotate the asteroid sprite each frame 
        based on the chosen rotation direction and the RotationSpeed value */
        SpinSprite();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /* play the asteroid sound effect immediately upon any collision 
        using the AudioSource. */
        audioSource.Play();

        /* if the object that collided with the asteroid is the player 
        (tagged "Player") attempt to get its GravitySwitch component
        if found call its FlipGravity() method to reverse the player's gravity */
        if (collision.gameObject.CompareTag("Player"))
        {
            GravitySwitch gs = collision.gameObject.GetComponent<GravitySwitch>();
            if (gs != null)
            {
                gs.FlipGravity();
            }
        }
    }

    void ChooseRotation()
    {
        /* Random.Range(1, 3) will return either 1 or 2
        if the value is 1 set rotationDirection to 1 (clockwise)
        otherwise set it to -1 (counterclockwise) */
        int choice = Random.Range(1, 3); // 1 or 2
        rotationDirection = (choice == 1) ? 1 : -1;
    }

    void SpinSprite()
    {
        /* apply a rotation to the asteroid's transform on the Z-axis each frame
        The rotation amount is determined by RotationSpeed, rotationDirection
        and Time.deltaTime to keep rotation speed consistent regardless of frame rate */
        transform.Rotate(0f, 0f, RotationSpeed * rotationDirection * Time.deltaTime);
    }
}
