using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 50f; // degrees per second
    private int rotationDirection; // 1 for clockwise, -1 for counterclockwise

    private AudioSource audioSource;
    void Start()
    {
        ChooseRotation();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        SpinSprite();
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

    void ChooseRotation()
    {
        // Random.Range with ints is inclusive for min, exclusive for max
        int choice = Random.Range(1, 3); // returns 1 or 2
        rotationDirection = (choice == 1) ? 1 : -1;
    }

    void SpinSprite()
    {
        transform.Rotate(0f, 0f, rotationSpeed * rotationDirection * Time.deltaTime);
    }
}
