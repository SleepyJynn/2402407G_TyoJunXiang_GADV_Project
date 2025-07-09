using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public string LeftControlKey;
    public string RightControlKey;
    public string SpaceControlKey;
    public GravitySwitch switchgravity;

    private bool grounded = false;

    void Start()
    {
        Debug.Log("Game starts");
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }
    void CheckIdle()
    {
        if (!Input.anyKey && grounded)
        {
            this.GetComponent<Animator>().SetInteger("Motion", 0);
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    void CheckMovement()
    {
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
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            this.GetComponent<SpriteRenderer>().flipX = true;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        if (Input.GetKey(RightControlKey))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            this.GetComponent<SpriteRenderer>().flipX = false;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        if (Input.GetKey(SpaceControlKey) && grounded == true)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 6f);
            grounded = false;
            this.GetComponent<Animator>().SetInteger("Motion", 2);
        }
    }
    void MovementUpsideDown()
    {
        if (Input.GetKey(LeftControlKey))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            this.GetComponent<SpriteRenderer>().flipX = false;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        if (Input.GetKey(RightControlKey))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            this.GetComponent<SpriteRenderer>().flipX = true;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("Motion", 1);
            }
        }
        if (Input.GetKey(SpaceControlKey) && grounded == true)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, -6f);
            grounded = false;
            this.GetComponent<Animator>().SetInteger("Motion", 2);
        }
    }
}