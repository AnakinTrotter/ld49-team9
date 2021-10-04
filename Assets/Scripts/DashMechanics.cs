using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMechanics : MonoBehaviour
{
    // Start is called before the first frame update
    //void Update()
    //{
    //    // dash cooldown
    //    if (PlayerController.dashTimer > 0)
    //    {
    //        PlayerController.dashTimer -= Time.deltaTime;
    //    }
    //}

    void DashStart()
    {
        PlayerController.IsDashing = true;
        HitDetection.hideHitbox = true;
        //Physics2D.IgnoreLayerCollision(7, 10);
    }

    void DashEnd()
    {
        PlayerController.IsDashing = false;
        HitDetection.hideHitbox = false;
        //Physics2D.IgnoreLayerCollision(7, 10, false);
        PlayerController.canDash = true;
        PlayerController.rollTimer = PlayerController.rollCooldown;
    }
}
