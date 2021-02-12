using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    //List <Collector> _collectors = new List<Collector>();
    public event Action OnPickedUp; // https://youtu.be/QjvqySSZGxg?t=150

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponents<Player>();
        if (player == null)
            return;

        gameObject.SetActive(false);
        OnPickedUp?.Invoke();
    }


}