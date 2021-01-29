using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    public float moveSpeed;

    //Ref to rigidbody
    private Rigidbody2D rb;

    //Direction
    private bool facingRight = true;

    //Inputs
    private float moveDirection;

    //Jump
    public float jumpForce;
    private bool isJumping = false;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    private bool isGrounded;
    public float checkRadius;
    private int jumpCount;
    public int maxJumpCount;

    private void Start()
    {
        jumpCount = maxJumpCount;
    }


    //Awake is called BEFORE the start method, while objects are initialized
    private void Awake()
    {
        //Will look for component Rigibody2D on script attachee
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player inputs
        ProcessInput();

        //Animate
        Animate();
    }

    private void FixedUpdate() // Physics Update method...Can be called multiple times per frame
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }

        // Move
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }
        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void ProcessInput()
    {
        moveDirection = UnityEngine.Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
