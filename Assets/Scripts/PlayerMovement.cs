using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    //setting up variables
    public string LeftControlKey;
    public string RightControlKey;
    public string SpaceControlKey;
    public GravitySwitch switchgravity;

    private bool grounded = false;
void Start()
    {
        Debug.Log("Game starts");
        //assign switchgravity to the GravitySwitch component so i can access it
        switchgravity = this.GetComponent<GravitySwitch>();
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
    //when collider triggers collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if collider has "ground" tag
        if (collision.gameObject.tag == "ground")
        {
            //set grounded to true
            grounded = true;
        }
    }

    void CheckIdle()
    {
        //if player is grounded and there is no input
        if (!Input.anyKey && grounded)
        {
            //set the animator to have it idle
            this.GetComponent<Animator>().SetInteger("Motion", 0);
            //set player x velocity to 0 so that it instantly stops
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void CheckMovement()
    {
        //if isFlipped from the switchgravity script is true
        //call the respective movements
        if (switchgravity.isFlipped == true)
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
        if (Input.GetKey(LeftControlKey))
        {
            //set player x velocity to -5 to move left
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            //flip sprite to face the correct direction
            this.GetComponent<SpriteRenderer>().flipX = true;
            if (grounded == true)
            {
                //set animator to move
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        if (Input.GetKey(RightControlKey))
        {
            //set player x velocity to 5 to move right
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            //flip sprite to face the correct direction
            this.GetComponent<SpriteRenderer>().flipX = false;
            if (grounded == true)
            {
                //set animator to move
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        if (Input.GetKey(SpaceControlKey) && grounded == true)
        {
            //set player y velocity to 6 to jump
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 6f);
            grounded = false;
            //set animator to jump
            this.GetComponent<Animator>().SetInteger("Motion", 2);
        }
    }
    void MovementUpsideDown()
    {
        if (Input.GetKey(LeftControlKey))
        {
            //set player x velocity to -5 to move left
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            //flip sprite to face the correct direction
            this.GetComponent<SpriteRenderer>().flipX = false;
            if (grounded == true)
            {
                //set animator to move
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        if (Input.GetKey(RightControlKey))
        {
            //set player x velocity to 5 to move right
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            //flip sprite to face the correct direction
            this.GetComponent<SpriteRenderer>().flipX = true;
            if (grounded == true)
            {
                //set animator to move
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        if (Input.GetKey(SpaceControlKey) && grounded == true)
        {
            //set player y velocity to 6 to jump since gravity is flipped
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, -6f);
            grounded = false;
            //set animator to jump
            this.GetComponent<Animator>().SetInteger("Motion", 2);
        }
    }
}