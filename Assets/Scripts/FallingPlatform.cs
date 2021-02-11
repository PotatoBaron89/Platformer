using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool PlayerInside;
    
    HashSet<Player> _playersInTrigger = new HashSet<Player>();
    Coroutine _coRoutine;
    Vector3 _initialPosition;
    
    [SerializeField] float _minWiggleX = -1.75f;
    [SerializeField] float _maxWiggleX = 1.75f;
    [SerializeField] float _minWiggleY = -0.05f;
    [SerializeField] float _maxWiggleY = 0.05f;
    [SerializeField] float _rateofDescent = 3f;
    bool _falling;


    void Start()
    {
        _initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("test");
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        _playersInTrigger.Add(player);
        
        PlayerInside = true;

        if (_playersInTrigger.Count == 1)
                _coRoutine = StartCoroutine(WiggleThenFall());
    }

    IEnumerator WiggleThenFall()
    {
        Debug.Log("Waiting to wiggle");
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Wiggling");
        float wiggleTimer = 0;
        while (wiggleTimer < 1)
        {
            float randomX = UnityEngine.Random.Range(_minWiggleX, _maxWiggleX);
            float randomY = UnityEngine.Random.Range(_minWiggleY, _maxWiggleY);
            transform.position = _initialPosition + new Vector3(randomX, randomY);

            float randomDelay = UnityEngine.Random.Range(0.05f, 0.1f);
            yield return new WaitForSeconds(randomDelay);
            wiggleTimer += randomDelay;
        }

        _falling = true;
        float fallTimer = 0;
        var colliders = GetComponents<Collider2D>();
        foreach (var allColliders in GetComponents<Collider2D>())
        
        {
            allColliders.enabled = false;
            
        }

        while (fallTimer < 3)
        {
            transform.position += Vector3.down * Time.deltaTime * _rateofDescent;
            fallTimer += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (_falling)
            return;
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;
        
        _playersInTrigger.Remove(player);
        if (_playersInTrigger.Count == 0)
        {
            PlayerInside = false;
            StopCoroutine(WiggleThenFall());
        }
        
    }
}
