using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed = 100f;
    private Rigidbody2D rb;

    private float seekTimer = 30f;
    private float seekCooldown = 30f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(seekTimer > 0) {
            seekTimer -= Time.deltaTime;
        } else {
            SeekPlayer();
            seekTimer = seekCooldown;
        }
    }

    void SeekPlayer() {
        rb.drag = 0;
        Vector2 dir = target.transform.position - transform.position;
        dir.Normalize();
        rb.velocity = speed * dir;
    }
}
