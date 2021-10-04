using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningPacifier : MonoBehaviour
{
    public static float collectSpeed = 8f;
    public static float collectThreshold = 4.55f;
    private Rigidbody2D rb;
    private GameObject baby;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        baby = GameObject.Find("Baby");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = baby.transform.position - this.transform.position;
        rb.velocity = dir / dir.magnitude * collectSpeed;
        if (dir.magnitude < collectThreshold)
        {
            Globals.babyRage--;
            Globals.currNumPacifiers--;
            Destroy(this.gameObject);
        }
    }
}
