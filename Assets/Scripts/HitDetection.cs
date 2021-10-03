using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private Collider2D coll;
    public static bool hideHitbox = false;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        if (hideHitbox)
        {
            coll.enabled = false;
        } 
        else
        {
            coll.enabled = true;
        }
    }
}
