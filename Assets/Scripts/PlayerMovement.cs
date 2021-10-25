using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;


    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    float gravityScaleAtStart;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    //OnMove is a method that takes the keyboard/controller input and calls this method
    //store the movement input and display it whenever this is called
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    //OnJump is a mehtod that takes the keyboard/controller input and calls this method
    void OnJump(InputValue value)
    {
        //only jump  when touching the ground
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return; }

        if (value.isPressed)
        {
            //jump
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void ClimbLadder()
    {
        //only climb when touching a ladder
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            //prevents a bug where the player continues the climbing animation if contiously holding up after falling off ladder
            myAnimator.SetBool("isClimbing", false);
            //resets the gravity when the player isn't climbing the ladder
            myRigidbody.gravityScale = gravityScaleAtStart;
            return; 
        }

        //use climbSpeed to control the speed at which the player moves
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, (moveInput.y * climbSpeed));
        myRigidbody.velocity = climbVelocity;
        //stops the player from sliding down the ladder when not moving
        myRigidbody.gravityScale = 0f;

        //bool is true when the absolute value of the player's y velocity is greater than 0 (or epsilon)
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

        //this will keep the player from getting stuck in the climb animation when he isn't moving
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    //Change the velocity of the player based on the movement input we got from OnMove()
    void Run()
    {
        //use runSpeed to control the speed at which the player moves
        Vector2 playerVelocity = new Vector2((moveInput.x * runSpeed), myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        //bool is true when the absolute value of the player's x velocity is greater than 0 (or epsilon)
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        //this will keep the player from getting stuck in the run animation when he isn't moving
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    //flip the sprite when the player moves to the left/right
    void FlipSprite()
    {
        //bool is true when the absolute value of the player's x velocity is greater than 0 (or epsilon)
        //this will keep the player from flipping back to the 'right' side when he isn't moving
        //this is due to the fact that unity counts 0 as a positive number; meaning that the player 
        //will always flip to the right when not moving without this code
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        //player will only flip when actively moving left/right
        if (playerHasHorizontalSpeed)
        {
            //the sprite will face the positive direction when moving on the positve x axis and
            //will face the negative direction when moving on the negative x axis
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
