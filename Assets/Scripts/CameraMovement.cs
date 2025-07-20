using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        MoveToPlayerX();
        MoveToPlayerY();
    }
    void MoveToPlayerX()
    {
        //if player's x position is not equals camera's x position
        if (GameObject.Find("Player").GetComponent<Transform>().position.x != this.GetComponent<Transform>().position.x)
        {
            //move camera to player's x position
            this.GetComponent<Transform>().position = new Vector3(GameObject.Find("Player").GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y, -10.0f);
        }
    }
    void MoveToPlayerY()
    {
        //if player's y position is not equals to camera's y position
        if (GameObject.Find("Player").GetComponent<Transform>().position.y != this.GetComponent<Transform>().position.y)
        {
            //move camera to player's y position
            this.GetComponent<Transform>().position = new Vector3(this.GetComponent<Transform>().position.x, GameObject.Find("Player").GetComponent<Transform>().position.y, -10.0f);
        }
    }
}
