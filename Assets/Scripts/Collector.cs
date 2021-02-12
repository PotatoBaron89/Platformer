using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] List <Collectible> _collectibles;
    [SerializeField] UnityEvent _onCollectionComplete;
    
    TMP_Text _remainingText;
    private int _countCollected;


    private void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
        foreach (var collectible in _collectibles)
        {
            collectible.AddCollector(this);
        }
    }

    public void ItemPickedUp()
    {
        _countCollected++;
        int countRemaining = _collectibles.Count - _countCollected;
        foreach (var collectible in _collectibles)
        {
            if (collectible.isActiveAndEnabled)
                countRemaining++;
        }
        
        if (_remainingText != null)
            _remainingText.SetText((countRemaining.ToString()));
        
        if (countRemaining > 0)
            return;
        
        _onCollectionComplete.Invoke();
    }
    
    void OnValidate()
    {
        _collectibles = _collectibles.Distinct().ToList();
    }


}




