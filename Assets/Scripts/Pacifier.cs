using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacifier : MonoBehaviour
{
    [SerializeField] float collectSpeed = 25;
    private Rigidbody2D rb;
    private GameObject depositPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        depositPos = GameObject.Find("PacifierSprite");
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.isKinematic)
        {
            Vector3 dir = depositPos.transform.position - this.transform.position;
            rb.velocity = dir.normalized * collectSpeed;
            Debug.Log(dir.magnitude);
            if (dir.magnitude < 8)
            {
                Globals.pacifiers++;
                Globals.currNumPacifiers--;
                if (Globals.currNumPacifiers < Globals.maxNumPacifiers)
                    Globals.shouldSpawn = true;
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Player"))
        {
            rb.isKinematic = true;
            // Debug.Log(Globals.pacifiers);
        }
    }
}
