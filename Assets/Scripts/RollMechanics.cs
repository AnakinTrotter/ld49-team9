using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollMechanics : MonoBehaviour
{

    void Update() {
        // roll cooldown
        if(PlayerController.rollTimer > 0) {
            PlayerController.rollTimer -= Time.deltaTime;
        }
    }
    
    void RollStart()
    {
        PlayerController.currentRollState = PlayerController.rollState.rollStart;
        HitDetection.hideHitbox = true;
        PlayerController.rollTimer = PlayerController.rollCooldown;
        Physics2D.IgnoreLayerCollision(7, 10);
        Debug.Log("Start");
    }

    void RollEnd()
    {
        PlayerController.currentRollState = PlayerController.rollState.rollEnd;
        Physics2D.IgnoreLayerCollision(7, 10, false);
        HitDetection.hideHitbox = false;
        Debug.Log("stop");
        
    }
}
