using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform posA;
    public Transform posB;
    bool moveDown = false;
    bool moveUp = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForMove();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Set the current object as Parent of “Player”
            collision.gameObject.transform.SetParent(this.gameObject.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Unparent “Player” from the current object
            collision.gameObject.transform.SetParent(null);
        }
    }

    void CheckForMove()
    {
        if (moveUp == true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, posB.position, Time.deltaTime * 2);
            if (this.transform.position == posB.position)
            {
                moveUp = false;
                moveDown = true;
            }
        }
        if (moveDown == true)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, posA.position, Time.deltaTime * 2);
            if (this.transform.position == posA.position)
            {
                moveUp = true;
                moveDown = false;
            }
        }
    }
}
