using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTime : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBonus = 5f;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play();
        if (collision.CompareTag("Player"))
        {
            GameUIManager uiManager = FindObjectOfType<GameUIManager>();
            uiManager.AddTime(timeBonus);
            Destroy(gameObject, audioSource.clip.length);
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
