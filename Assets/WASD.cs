using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
    public float collectedScore = 0f;
    //accel is public so we can fine tune the player controller from the editor
    //separate horizontal and vertical accelerations
    public float horizontal = 1f;
    public float speed = 1f;
    public float jumpForce = 30f;

    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;
    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] Rigidbody2D myRB;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, jumpForce);

            jumpBufferCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && myRB.velocity.y > 0f)
        {
            myRB.velocity = new Vector2(myRB.velocity.x, myRB.velocity.y * 0.5f);

            coyoteTimeCounter = 0f; 
        }
    }
    // FixedUpdate is called every physics update
    //it is a void function, so it does not return any data
    void FixedUpdate()
    {
        myRB.velocity = new Vector2(horizontal * speed, myRB.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //checking for enemy or collectible collisions
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("player collided with " + collision.gameObject.name);

        //when we collide with something collectible, destroy it and increment the player score
        if (collision.gameObject.tag == "Collectible")
        {
            Destroy(collision.gameObject);
            collectedScore++;
        }
    }
}
