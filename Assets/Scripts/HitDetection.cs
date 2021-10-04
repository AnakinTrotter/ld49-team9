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
        if (hideHitbox) // To handle roll invincibility
        {
            coll.enabled = false;
        } 
        else
        {
            coll.enabled = true;
        }
    }
}
