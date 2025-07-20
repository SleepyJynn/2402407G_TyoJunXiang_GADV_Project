using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform posA;
    public Transform posB;
    private bool moveDown = false;
    private bool moveUp = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForMove();
    }
    //when collider collides with platform
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if collider has "Player" tag
        if (collision.gameObject.tag == "Player")
        {
            //set the platform as the parent of “Player”
            collision.gameObject.transform.SetParent(this.gameObject.transform);
        }
    }

    //when collider exits collision with platform
    void OnCollisionExit2D(Collision2D collision)
    {
        //if collider has "Player" tag
        if (collision.gameObject.tag == "Player")
        {
            //unparent the platform from the "Player"
            collision.gameObject.transform.SetParent(null);
        }
    }

    void CheckForMove()
    {
        //if the next step is to moveUp
        if (moveUp == true)
        {
            //move platform towards the other position
            this.transform.position = Vector2.MoveTowards(this.transform.position, posB.position, Time.deltaTime * 2);
            //if platform reaches the other position
            if (this.transform.position == posB.position)
            {
                //current step is now false, next step is now true
                moveUp = false;
                moveDown = true;
            }
        }
        //if the next step is to moveDown
        if (moveDown == true)
        {
            //move platform towards the other position
            this.transform.position = Vector2.MoveTowards(this.transform.position, posA.position, Time.deltaTime * 2);
            //if platform reaches the other position
            if (this.transform.position == posA.position)
            {
                //current step is now false, next step is now true
                moveUp = true;
                moveDown = false;
            }
        }
    }
}
