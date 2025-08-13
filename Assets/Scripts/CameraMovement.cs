using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerTransform; // reference to the player's Transform

    void Start()
    {
        /* find the Player object by name once at the start 
        and store its Transform component for faster access in Update()
        this avoids calling GameObject.Find() every frame */
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        /* update the camera's X and Y positions independently each frame 
        to match the player's position, while keeping the Z position fixed 
        at -10 so the camera remains in front of the scene */
        MoveToPlayerX();
        MoveToPlayerY();
    }

    void MoveToPlayerX()
    {
        /* if the camera’s X position is not the same as the player’s X position 
        update only the X value of the camera’s position to match the player’s X */
        if (playerTransform.position.x != transform.position.x)
        {
            transform.position = new Vector3(playerTransform.position.x, transform.position.y, -10.0f);
        }
    }

    void MoveToPlayerY()
    {
        /* if the camera’s Y position is not the same as the player’s Y position 
        update only the Y value of the camera’s position to match the player’s Y */
        if (playerTransform.position.y != transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, playerTransform.position.y, -10.0f);
        }
    }
}
