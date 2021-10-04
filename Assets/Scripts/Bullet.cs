using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static float bulletSpeedMult = 1f;
    public float rotateSpeed = 0f;
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
        this.transform.Rotate(new Vector3(0, 0, rotateSpeed));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Cannon")
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision);
        else
        {
            if (tag == "Player") 
                if(PlayerLife.TakeDamage())
                    Destroy(gameObject);
        }
    }

    public Rigidbody2D getRB()
    {
        return rb;
    }
}
