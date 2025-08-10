using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorPointer : MonoBehaviour
{
    public Transform player;
    public Transform exitDoor;

    private RectTransform arrowRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        arrowRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = exitDoor.position - player.position;
        direction.z = 0; // Ignore Z axis for 2D UI arrow rotation

        if (direction != Vector3.zero)
        {
            arrowRectTransform.up = direction.normalized;
        }
    }
}
