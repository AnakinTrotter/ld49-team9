using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public Transform player;
    public Transform cannon;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(player.position.x > cannon.position.x)
            rb.velocity = transform.right*speed;
        else
            rb.velocity = transform.right*speed*-1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
