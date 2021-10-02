using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    private float speed = 0f;
    public static bool IsRolling = false;
    private float rollDir;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float rollDistance = 20f;

    private enum MovementState { idle, running, jumping, falling }
    [SerializeField] private AudioSource jumpSoundEffect;

    private int jumpBufferCounter; 
    private int bufferMax = 10;         // max number of frames to allow before executing jump
    private float coyoteTimer;
    [SerializeField] private float coyoteMaxTime = 0.5f; // in seconds
    private bool wasGrounded;           // for coyote time
    private bool timerStart;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Globals.debuffs.Add(Globals.DebuffState.invert);
        // Globals.debuffs.Add(Globals.DebuffState.slow);
        jumpBufferCounter = 100;
        coyoteTimer = 0f;
        wasGrounded = false;
        timerStart = false;
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        speed = moveSpeed;

        // account for debuffs
        if(Globals.debuffs.Contains(Globals.DebuffState.invert))
            dirX *= -1;
        if(Globals.debuffs.Contains(Globals.DebuffState.slow))
            speed /= 2;

        if (Input.GetKeyDown("f") && IsGrounded())
        {
            rollDir = dirX;
            anim.SetTrigger("roll");
        }

        if (IsRolling)
        {
            rb.velocity = new Vector2(rollDir * rollDistance, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }

        if (Input.GetButton("Jump"))
        {
            jumpBufferCounter = 0;
        }
        
        // START Coyote Time logic
        if (IsGrounded() && rb.velocity.y == 0)  // to make sure player is not in process of jumping
        {
            wasGrounded = true;
        }
        else
        {
            if (wasGrounded)                     // if just left edge, start timer
            {
                timerStart = true;
                wasGrounded = false;
            }
        }
        if (coyoteTimer < coyoteMaxTime && timerStart)         // while coyote time   
        {
            if (Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                timerStart = false;
                coyoteTimer = 0;
            }
            else
                coyoteTimer += Time.deltaTime;
        }
        else
        {
            timerStart = false;
            coyoteTimer = 0;
        }
        // END Coyote Time logic

        if (jumpBufferCounter < bufferMax)
        {
            jumpBufferCounter += 1;
            if (IsGrounded())
            {
                // jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}