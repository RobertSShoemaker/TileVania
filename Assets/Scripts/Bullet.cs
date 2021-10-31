using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;

    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        
        //bullet will fire in the direction that player is facing
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        //fire the bullet at the speed of xSpeed
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    //destroy the enemy and the bullet when they collide
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    //destroy the bullet when it collides with anything
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
