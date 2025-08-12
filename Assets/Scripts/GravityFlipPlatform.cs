using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration = 1f;

    private Coroutine currentFlip;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
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
