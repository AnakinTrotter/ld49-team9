using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static float speed = 10f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x) > 100) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Cannon")
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision);
        else
        {
            if (tag == "Player")
                PlayerLife.TakeDamage();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }

    public Rigidbody2D getRB()
    {
        return rb;
    }
}
