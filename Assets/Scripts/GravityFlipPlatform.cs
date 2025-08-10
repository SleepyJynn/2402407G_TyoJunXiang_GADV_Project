using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration = 1f;

    private Coroutine currentFlip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the GravitySwitch script and call the same method
            GravitySwitch gs = collision.gameObject.GetComponent<GravitySwitch>();
            if (gs != null)
            {
                gs.FlipGravity();
            }
        }
    }
}
