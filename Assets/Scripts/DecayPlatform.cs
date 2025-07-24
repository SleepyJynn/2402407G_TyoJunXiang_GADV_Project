using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private bool triggered = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggered && collision.gameObject.tag == "Player")
        {
            triggered = true;
            StartCoroutine(Decay());
        }
    }

    IEnumerator Decay()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
