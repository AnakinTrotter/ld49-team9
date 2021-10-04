using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacifier : MonoBehaviour
{
    public static float collectSpeed = 10f;
    public static float collectThreshold = 5.55f;
    private Rigidbody2D rb;
    private GameObject depositPos;
    private float origSize, origDist;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        depositPos = GameObject.Find("PacifierSprite");
        origSize = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.isKinematic)
        {
            Vector3 dir = depositPos.transform.position - this.transform.position;
            float size = (dir.magnitude - collectThreshold/2) / (origDist - collectThreshold/2) * origSize;
            size = Mathf.Min(origSize, Mathf.Max(size, origSize / 2));
            transform.localScale = new Vector3(size, size, 1);
            rb.velocity = dir / dir.magnitude * collectSpeed;
            Debug.Log(dir.magnitude + "/" + origDist);
            if (dir.magnitude < collectThreshold)
            {
                Globals.pacifiers++;
                // Globals.currNumPacifiers--;
                // if (Globals.currNumPacifiers < Globals.maxNumPacifiers)
                //     Globals.shouldSpawn = true;
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name.Equals("Player"))
        {
            Vector3 dir = depositPos.transform.position - this.transform.position;
            origDist = dir.magnitude;
            rb.isKinematic = true;
            // Debug.Log(Globals.pacifiers);
        }
    }
}
