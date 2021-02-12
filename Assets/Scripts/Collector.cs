using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Collectible[] _collectibles;

    private void Update()
    {
        foreach (var collectible in _collectibles)
            if (collectible.isActiveAndEnabled)
                return;
    }
}




