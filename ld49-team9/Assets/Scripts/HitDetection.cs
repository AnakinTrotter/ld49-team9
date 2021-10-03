using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private Collider2D coll;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        if (PlayerController.IsRolling)
        {
            coll.enabled = false;
        } else
        {
            coll.enabled = true;
        }
    }
}
