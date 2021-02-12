using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponents<Player>();
        if (player == null)
            return;
        
        gameObject.SetActive(false);
    }
}