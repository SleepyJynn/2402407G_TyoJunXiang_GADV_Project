using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    bool grounded = false;
    public string LeftControlKey;
    public string RightControlKey;
    public string SpaceControlKey;

    private bool upsidedown = false;
    public GravitySwitch switchgravity;

    void Start()
    {
        Debug.Log("Game starts");
        switchgravity = this.GetComponent<GravitySwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        Debug.Log(grounded);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            grounded = true;
        }
    }

    void CheckMovement()
    {
        if (Input.GetKey(LeftControlKey))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            this.GetComponent<SpriteRenderer>().flipX = true;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("motion", 1);
            }
        }
        if (Input.GetKey(RightControlKey))
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(5.0f, this.GetComponent<Rigidbody2D>().velocity.y);
            this.GetComponent<SpriteRenderer>().flipX = false;
            if (grounded == true)
            {
                this.GetComponent<Animator>().SetInteger("motion", 1);
            }
        }
        if (Input.GetKey(SpaceControlKey) && grounded == true)
        {
            if (switchgravity.isFlipped == true)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, -6f);
                grounded = false;
                this.GetComponent<Animator>().SetInteger("motion", 2);
            }
            else
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 6f);
                grounded = false;
                this.GetComponent<Animator>().SetInteger("motion", 2);
            }
        }
        if (!Input.anyKey && grounded == true)
        {
            this.GetComponent<Animator>().SetInteger("motion", 0);
        }
    }
    void FixedUpdate()
    {
        if (!Input.anyKey && grounded)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}