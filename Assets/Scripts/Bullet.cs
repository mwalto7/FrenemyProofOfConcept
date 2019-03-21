﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    public Rigidbody2D rb;
    public float damage = 5f;
    public GameObject impactEffect;//create impact effect and put it here
    private bool hasRegistered;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        Destroy(gameObject, 3f);//destroys bullet after 3seconds
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hasRegistered is the only way to prevent the bullet from registering
        //twice as there are two colliders for each player (circle and box)
        if (!hasRegistered)
        {
            hasRegistered = true;
            if(collision.transform.tag == "Player")
            {
                Destroy(gameObject);
                Player player = collision.GetComponent<Player>();
                if (player != null)
                {
                    player.Damage(damage, collision.gameObject);
                }
            }
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);

    }

}
