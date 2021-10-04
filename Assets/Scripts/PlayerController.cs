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
    public static bool IsRolling = false;
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
    private int jumpsBuffered;
    private int bufferMax = 10;         // max number of frames to allow before executing jump
    private float coyoteTimer;
    [SerializeField] private float coyoteMaxTime = 0.5f; // in seconds
    private bool wasGrounded;           // for coyote time
    private bool timerStart;
    private bool hasDoubleJump;
    public static bool onGround;
    public static bool canDash = true;

    public GameObject scanner;
    public float scanTimer, scanCooldown = 10f;

    public static float rollCooldown = 0.6f;
    public static float rollTimer;


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
        // Globals.debuffs.Add(Globals.DebuffState.blind);
        jumpBufferCounter = 100;
        coyoteTimer = 0f;
        wasGrounded = false;
        timerStart = false;
        hasDoubleJump = true;
        jumpsBuffered = 0;
        rollTimer = rollCooldown;
        gravity = rb.gravityScale;
        prevPos = transform.position;
        backTimer = backCooldown;
        Globals.debuffs.Clear();
    }

    // Update is called once per frame
    private void Update()
    {
        if(transform.position.y < -20) {
            PlayerLife.Die();
        }
        dirX = Input.GetAxisRaw("Horizontal");
        speed = moveSpeed;
        scanTimer = Mathf.Min(scanTimer + Time.deltaTime, scanCooldown);
        rb.gravityScale = gravity;

        // account for debuffs
        if(Globals.debuffs.Contains(Globals.DebuffState.invert)) {
            dirX *= -1;
            // Debug.Log("invert");
        }
        if(Globals.debuffs.Contains(Globals.DebuffState.slow)) {
            speed /= 2;
            // Debug.Log("slow");
        }
        if(Globals.debuffs.Contains(Globals.DebuffState.moon)) {
            rb.gravityScale = gravity / 4;
            // Debug.Log("moon");
        }
        if(Globals.debuffs.Contains(Globals.DebuffState.fast)) {
            speed *= 2;
            // Debug.Log("fast ");
        }

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

        onGround = IsGrounded();

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
        if (Input.GetButtonDown("Fire3") && !onGround && rollTimer <= 0 && canDash)
        {
            anim.SetTrigger("dash");
            canDash = false;
            dashDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
        if (!IsRolling)
        {
            if (Mathf.Abs(rb.velocity.x) < speed && rb.velocity.x * dirX > 0)
            {
                rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
            }
            else if (rb.velocity.x * dirX < 0)
            {
                rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
            }
            else if (rb.velocity.x == 0 && dirX != 0)
            {
                rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
            }
            else if (rb.velocity.x != 0 && dirX == 0 && !IsDashing)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        // Roll physics
        if (currentRollState == rollState.rollStart)
        {
            rb.velocity = new Vector2(rb.velocity.x + rollDir * rollSpeed, rb.velocity.y);
            IsRolling = true;
            currentRollState = rollState.idle;
        }

        if (currentRollState == rollState.rollEnd)
        {
            rb.velocity = new Vector2(rb.velocity.x - rollDir * rollSpeed, rb.velocity.y);
            IsRolling = false;
            currentRollState = rollState.idle;
        }
        // End roll physics


        // START Coyote Time logic
        if (onGround && rb.velocity.y == 0)  // to make sure player is not in process of jumping
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

        // Jump logic
        if (onGround)
        {
            hasDoubleJump = true;  // reset for double jump
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = 0;
            jumpsBuffered++;
            if (!onGround && hasDoubleJump) 
            {
                hasDoubleJump = false;
                jumpsBuffered = 0;
                DoubleJump();
            }
        }

        if (jumpBufferCounter < bufferMax)
        {
            jumpBufferCounter += 1;
            if (onGround)
            { 
                // jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if (jumpsBuffered > 1)
                {
                    DoubleJump();
                    hasDoubleJump = false;
                }
                jumpsBuffered = 0;
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
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
    }

    private void DoubleJump()
    {
        anim.SetTrigger("djump");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, new Vector2(coll.bounds.size.x * 0.75f, 
            coll.bounds.size.y), 0f, Vector2.down, .1f, jumpableGround);
    }

    public static void DamageAnim()
    {
        GameObject player = GameObject.Find("Player");
        SpriteRenderer sprite = player.GetComponent<SpriteRenderer>();
        player.GetComponent<PlayerController>().StartCoroutine(PlayerController.playerFade(sprite));
    }

    public static IEnumerator playerFade(SpriteRenderer sprite)
    {
        float alpha = 0.25f;
        while (alpha < 0.75f)
        {
            sprite.color = new Color(1, alpha, alpha, 0.25f + alpha);
            alpha += 0.5f / 2f * Time.deltaTime;
            yield return null;
        }
        sprite.color = new Color(1f, 1f, 1f, 1f);
    }
}