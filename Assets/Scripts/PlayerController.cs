using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private Transform player;

    [SerializeField] private LayerMask jumpableGround;

    // movement
    private float dirX = 0f;
    private float prevDirX;
    private float speed = 0f;

    public enum rollState { idle, rollStart, rollEnd }
    public static rollState currentRollState = rollState.idle;
    private float rollDir;
    private Vector2 dashDir;
    public enum dashState { idle, dashStart, dashEnd }
    public static dashState currentDashState = dashState.idle;
    public static bool IsDashing = false;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float rollSpeed = 20f;
    [SerializeField] private float dashSpeed = 20f;

    private enum MovementState { idle, running, jumping, falling }
    [SerializeField] private AudioSource jumpSoundEffect;

    private int jumpBufferCounter; 
    private int bufferMax = 10;         // max number of frames to allow before executing jump
    private float coyoteTimer;
    [SerializeField] private float coyoteMaxTime = 0.5f; // in seconds
    private bool wasGrounded;           // for coyote time
    private bool timerStart;
    private bool hasDoubleJump;

    public GameObject scanner;
    public float scanTimer, scanCooldown = 10f;

    public static float rollCooldown = 1f;
    public static float rollTimer;
    public static float dashCooldown = 1f;
    public static float dashTimer;


    private float gravity;

    // rewind fields
    private Vector2 prevPos;
    private float backCooldown = 4.0f;
    private float backTimer;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GetComponent<Transform>();
        // Globals.debuffs.Add(Globals.DebuffState.invert);
        // Globals.debuffs.Add(Globals.DebuffState.slow);
        // Globals.debuffs.Add(Globals.DebuffState.moon);
        // Globals.debuffs.Add(Globals.DebuffState.fast);
        // Globals.debuffs.Add(Globals.DebuffState.rewind);
        jumpBufferCounter = 100;
        coyoteTimer = 0f;
        wasGrounded = false;
        timerStart = false;
        hasDoubleJump = true;
        rollTimer = rollCooldown;
        gravity = rb.gravityScale;
        prevPos = transform.position;
        backTimer = backCooldown;
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        speed = moveSpeed;
        scanTimer = Mathf.Min(scanTimer + Time.deltaTime, scanCooldown);
        rb.gravityScale = gravity;

        // account for debuffs
        if(Globals.debuffs.Contains(Globals.DebuffState.invert))
            dirX *= -1;
        if(Globals.debuffs.Contains(Globals.DebuffState.slow))
            speed /= 2;
        if(Globals.debuffs.Contains(Globals.DebuffState.moon))
            rb.gravityScale = gravity / 6;
        if(Globals.debuffs.Contains(Globals.DebuffState.fast))
            speed *= 4;

        // time reverse timer logic
        if(backTimer > 0) {
            backTimer -= Time.deltaTime;
        } else {
            backTimer = backCooldown;
            if(Globals.debuffs.Contains(Globals.DebuffState.rewind)) {
                Vector2 temp = transform.position;
                transform.position = prevPos;
                prevPos = temp;
            } else {
                prevPos = transform.position;
            }
        }

        // Start roll logic
        if (Input.GetButtonDown("Fire3") && IsGrounded() && rollTimer <= 0)
        {
            if (sprite.flipX) {
                rollDir = -1;
            } else {
                rollDir = 1;
            }
            anim.SetTrigger("roll");
        }

        // Start: Aerial dash logic
        if (Input.GetButtonDown("Fire3") && !IsGrounded() && dashTimer <= 0)
        {
            dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Debug.Log(dashDir.ToString());
            anim.SetTrigger("dash");
        }
        if (IsDashing)
        {
            rb.velocity = new Vector2(dashDir.x * dashSpeed, dashDir.y * dashSpeed);
        }
        // End: Aerial dash logic

        // Scanner logic
        if (Input.GetKeyDown("e") && scanTimer >= scanCooldown)
        {
            Instantiate(scanner, this.transform);
            scanTimer = 0;
        }

        // Improved horizontal movement
        if (Mathf.Abs(rb.velocity.x) < moveSpeed && rb.velocity.x * dirX > 0)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        } else if (rb.velocity.x * dirX < 0)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        } else if (rb.velocity.x == 0 && dirX != 0)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        } else if (rb.velocity.x != 0 && dirX == 0 && !IsDashing)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Roll physics
        if (currentRollState == rollState.rollStart)
        {
            rb.velocity = new Vector2(rb.velocity.x + rollDir * rollSpeed, rb.velocity.y);
            currentRollState = rollState.idle;
        }

        if (currentRollState == rollState.rollEnd)
        {
            rb.velocity = new Vector2(rb.velocity.x - rollDir * rollSpeed, rb.velocity.y);
            currentRollState = rollState.idle;
        }
        // End roll pjysics

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

        // Double jump logic
        if (IsGrounded())
        {
            hasDoubleJump = true;  // reset for double jump
        }

        if (Input.GetButtonDown("Jump") && !IsGrounded() && hasDoubleJump)
        {
            hasDoubleJump = false;
            anim.SetTrigger("djump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        return Physics2D.BoxCast(coll.bounds.center, new Vector2(coll.bounds.size.x * 0.75f, 
            coll.bounds.size.y), 0f, Vector2.down, .1f, jumpableGround);
    }
}