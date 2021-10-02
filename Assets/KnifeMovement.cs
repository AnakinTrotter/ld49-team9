using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform knife;
    private Animator anim;
    [SerializeField] private bool facingLeft;
    private float dirX = 1f;
    [SerializeField] private float velocity = 12f;
    private Vector2 dir;
    [SerializeField] private float detectionRange = 20f;
    private bool IsAttacking = false;
    private LayerMask targLayer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        knife = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        dirX = facingLeft ? -1 : 1;
        dir = new Vector2(dirX, 0);
        targLayer = LayerMask.GetMask("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Contact - hostile shooter!");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = new Vector2(knife.position.x, knife.position.y);
        if (!IsAttacking && Physics2D.Raycast(pos, dir, detectionRange, targLayer))
        {
            Debug.Log("Enemy Spotted!");
            anim.SetTrigger("attack");
        } 
        if (Mathf.Abs(knife.position.x) >= 100)
        {
            Destroy(gameObject);
        }
    }

    void Attack()
    {
        IsAttacking = true;
        rb.velocity = new Vector2(dirX * velocity, 0);
    }

    private void OnDestroy()
    {
        // setup destroy animation in animator
    }
}
