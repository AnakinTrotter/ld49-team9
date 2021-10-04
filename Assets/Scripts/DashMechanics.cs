using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMechanics : MonoBehaviour
{
    private bool recharge = true;
    void Update()
    {
        // dash cooldown
        if (PlayerController.rollTimer > 0 && recharge)
            PlayerController.rollTimer -= Time.deltaTime;
    }

    void DashStart()
    {
        PlayerController.IsDashing = true;
        HitDetection.hideHitbox = true;
        //Physics2D.IgnoreLayerCollision(7, 10);
        recharge = false;
    }

    void DashEnd()
    {
        PlayerController.IsDashing = false;
        HitDetection.hideHitbox = false;
        //Physics2D.IgnoreLayerCollision(7, 10, false);
        PlayerController.canDash = true;
        PlayerController.rollTimer = PlayerController.rollCooldown;
        Debug.Log(PlayerController.onGround);
        StartCoroutine(WaitForGround());
    }

    IEnumerator WaitForGround()
    {
        while (!PlayerController.onGround)
        {
            // Debug.Log(PlayerController.onGround);
            yield return null;
        }
        recharge = true;
    }
}
