using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("Player died");
            player.ResetToStart();
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("Player died");
            player.ResetToStart();
        }
    }
}
