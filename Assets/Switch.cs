using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Switch : MonoBehaviour
{
    [SerializeField] Sprite _spriteLeft;
    [SerializeField] Sprite _spriteRight;
    SpriteRenderer _spriteRenderer;
    private float distance;
    private bool isLeft;
    private bool isTouching;

    private Vector3 player = new Vector3();
    private Vector3 self = new Vector3();
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        self = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
       // if (Vector3.Distance(transform.position, gameObject<Player>.transform.position) < 1)
        //    print("touching");
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject.transform.position;
         
        //Vector3.Distance(transform.position, player);

        if (player.x < self.x && isLeft == false)
        {
            //_spriteRenderer.sprite = _spriteLeft;
            isLeft = true;
            print("Is left");
        }
        else if (player.x > self.x && isLeft == true)
        {
            //_spriteRenderer.sprite = _spriteRight;
            isLeft = false;
            print("is right");
        }
    }*/

    private void OnTriggerExit2D(Collider2D other)
    {
        player = other.gameObject.transform.position;
        if (player.x < self.x && isLeft == false)
        {
            isLeft = true;
            print("Is left");
            if (_spriteLeft != null)
                _spriteRenderer.sprite = _spriteLeft;
        }
        else if (player.x > self.x && isLeft == true)
        {
            if (_spriteRight != null)
                _spriteRenderer.sprite = _spriteRight;
            isLeft = false;
            print("is right");
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        
       
    }
}
