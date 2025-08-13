using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLoseSound : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource audioSource; // AudioSource attached to this object for playing lose sound
    private GameUIManager uiManager; // reference to GameUIManager to check if player has lost
    private bool hasPlayed = false; // ensures the lose sound plays only once
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // assign the AudioSource component attached to this object

        uiManager = FindObjectOfType<GameUIManager>(); // find the GameUIManager in the scene to access HasLost flag
    }

    // Update is called once per frame
    void Update()
    {
        CheckForLoss();
    }

    void CheckForLoss()
    {
        // if the player has lost and the sound has not yet played
        if (uiManager.HasLost == true && hasPlayed == false)
        {
            audioSource.Play();
            hasPlayed = true; // mark sound as played so it doesn’t repeat
        }
    }
}
