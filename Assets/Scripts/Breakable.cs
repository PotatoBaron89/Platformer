using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<Player>() == null)
            return;

        if (col.contacts[0].normal.y > 0)
            TakeHit();
    }

    void TakeHit()
    {
        var particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Play();
        
        //gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;


    }
}
