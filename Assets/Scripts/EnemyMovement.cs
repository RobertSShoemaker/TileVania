using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        myRigidbody.velocity = new Vector2(moveSpeed, 0);
    }

    //enemy will change direction when it reaches a wall or ledge
    void OnTriggerExit2D(Collider2D collision)
    {
        //move in the opposite direction
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    //flip the sprite in the movement direction
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
    }
}
