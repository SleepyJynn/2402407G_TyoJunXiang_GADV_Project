using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public GravitySwitch switchgravity; // Reference to GravitySwitch to check gravity state
    public ParticleSystem jumpDust; // Particle system for jump effect

    private bool grounded = false; // Tracks if the player is touching the ground
    private AudioSource audioSource; // AudioSource for jump sounds

    void Start()
    {
        // assign switchgravity to the GravitySwitch component so i can access it
        switchgravity = this.GetComponent<GravitySwitch>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
    }
    void FixedUpdate()
    {
        CheckIdle();
    }

    // Called when collider starts touching another collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If player collides with ground, set grounded to true
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

    void CheckIdle()
    {
        // If no key is pressed and player is grounded
        if (!Input.anyKey && grounded)
        {
            // Set animator to idle state
            this.GetComponent<Animator>().SetInteger("Motion", 0);
            // Stop horizontal movement
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void CheckMovement()
    {
        // Check gravity flip state and call appropriate movement function
        if (switchgravity.IsFlipped == true)
        {
            MovementUpsideDown();
        }
        else
        {
            Movement();
        }
    }
    void Movement()
    {
        // Move left
        if (Input.GetKey(KeyCode.A))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, this.GetComponent<Rigidbody2D>().velocity.y);

            this.GetComponent<SpriteRenderer>().flipX = true;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        // Move right
        if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, this.GetComponent<Rigidbody2D>().velocity.y);

            this.GetComponent<SpriteRenderer>().flipX = false;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        // Jump
        if (Input.GetKey(KeyCode.Space) && grounded == true)
        {
            audioSource.Play();
            jumpDust.Play();

            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 6f);

            grounded = false;

            this.GetComponent<Animator>().SetInteger("Motion", 2);
        }
    }
    void MovementUpsideDown()
    {
        // Move left (flipped gravity)
        if (Input.GetKey(KeyCode.A))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            
            this.GetComponent<SpriteRenderer>().flipX = false;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        // Move right (flipped gravity)
        if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            
            this.GetComponent<SpriteRenderer>().flipX = true;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        // Jump (flipped gravity)
        if (Input.GetKey(KeyCode.Space) && grounded == true)
        {
            audioSource.Play();
            jumpDust.Play();
            
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, -6f);

            grounded = false;
            
            this.GetComponent<Animator>().SetInteger("Motion", 2);
        }
    }
}