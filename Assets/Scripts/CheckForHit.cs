using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForHit : MonoBehaviour
{
    // Start is called before the first frame update

    public ParticleSystem hitImpactFx; // reference to the particle effect that plays when a collision occurs
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        /* when this GameObject collides with another object play the particle effect to show a hit impact */
        hitImpactFx.Play();
    }

}
