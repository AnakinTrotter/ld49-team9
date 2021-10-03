using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMechanics : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        // dash cooldown
        if (PlayerController.dashTimer > 0)
        {
            PlayerController.dashTimer -= Time.deltaTime;
        }
    }

    void DashStart()
    {
        PlayerController.IsDashing = true;
        HitDetection.hideHitbox = true;
    }

    void DashEnd()
    {
        PlayerController.IsDashing = false;
        HitDetection.hideHitbox = false;
        PlayerController.dashTimer = PlayerController.dashCooldown;
    }
}
