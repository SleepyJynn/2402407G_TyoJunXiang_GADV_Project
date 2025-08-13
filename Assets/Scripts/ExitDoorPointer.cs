using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorPointer : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Player; // the player transform that the arrow will point from
    public Transform ExitDoor; // the exit door transform that the arrow will point to

    private RectTransform arrowRectTransform;

    void Start()
    {
        /* store the RectTransform of the UI arrow once at start
        so we can rotate it every frame without repeatedly calling GetComponent */
        arrowRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        ComputeDirection();
    }

    void ComputeDirection()
    {
        /* compute direction from player to exit door in world space
        ignore Z to keep it in the 2D UI plane then rotate the arrow
        to point along that vector */
        Vector3 direction = ExitDoor.position - Player.position;
        direction.z = 0; // ignore Z axis for 2D UI arrow rotation

        if (direction != Vector3.zero)
        {
            arrowRectTransform.up = direction.normalized;
        }
    }
}
