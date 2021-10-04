using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningPacifier : MonoBehaviour
{
    [SerializeField] float returnSpeed = 25;
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
        rb.velocity = dir.normalized * returnSpeed;
        if (dir.magnitude < 8)
        {
            Globals.babyRage--;
            Destroy(this.gameObject);
        }
    }
}
